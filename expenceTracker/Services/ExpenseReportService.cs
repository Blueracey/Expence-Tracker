
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using expenceTracker.Data;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;

namespace expenceTracker.Services
{
    public class ExpenseReportService
    {
        private readonly AppDatabaseContext _context;

        public ExpenseReportService(AppDatabaseContext context)
        {
            _context = context;
        }
        public async Task<byte[]> GenerateExpenseMonthReport(int id)
        {
            var expenseMonth = await _context.monthlyExpence
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();

            if (expenseMonth == null)
            {
                throw new ArgumentException("Expense Month not found");
            }

            var expectedExpenses = await _context.expectedExpence
                .Where(e => e.expenceId == id)
                .ToListAsync();

            //separate expenses based on type
            var variableExpectedExpenses = expectedExpenses.Where(e => e.type == "Variable").ToList();
            var recurringExpectedExpenses = expectedExpenses.Where(e => e.type == "Recurring").ToList();

            var actualExpenses = await _context.actualExpences
                .Where(e => e.expenceId == id)
                .ToListAsync();

            //get sum of expected and actual expense
            var totalExpectedExpense = expectedExpenses.Any() ? expectedExpenses.Sum(e => e.predictedCost) : 0;
            var totalActualExpense = actualExpenses.Any() ? actualExpenses.Sum(e => e.finalCost) : 0;

            var pdf = Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Margin(1, Unit.Inch);

                    page.Header()
                        .Text("Expense Month Report")
                        .FontSize(11)
                        .Bold()
                        .FontSize(18);

                    page.Content().Column(column =>
                    {
                        column.Spacing(0.5f, Unit.Centimetre);

                        column.Item().Column(column =>
                        {
                            column.Item().Text($"Date: {expenseMonth.date:MMMM yyyy}");
                            column.Item().Text($"Budget: ${expenseMonth.budget:0.00}");
                            column.Item().Text($"Total Expected Expense: ${totalExpectedExpense:0.00}");
                            column.Item().Text($"Total Actual Expense: ${totalActualExpense:0.00}");
                        });

                        column.Item().Column(column =>
                        {
                            column.Item().Text($"Variable Expected Expenses")
                            .Bold()
                            .FontSize(14);

                            foreach (var expense in variableExpectedExpenses)
                            {
                                column.Item().Text($" > {expense.name}: ${expense.predictedCost:0.00}");
                            }
                        });

                        column.Item().Column(column =>
                        {
                            column.Item().Text($"Recurring Expected Expenses")
                           .Bold()
                           .FontSize(14);

                            column.Item().Text($"*The cost presented is the calculated amount for the whole month")
                                .Italic()
                                .FontSize(8);

                            foreach (var expense in recurringExpectedExpenses)
                            {
                                column.Item().Text($" > {expense.name}: ${expense.predictedCost:0.00} : {expense.frequency}");
                            }
                        });

                        column.Item().Column(column =>
                        {
                            column.Item().Text($"Actual Expenses")
                                .Bold()
                                .FontSize(14);

                            foreach (var payment in actualExpenses)
                            {
                                column.Item().Text($" > {payment.category}: ${payment.finalCost:0.00} : {payment.datePayed:MMMM dd yyyy}");
                            }
                        });
                    });
                });
            });

            return pdf.GeneratePdf();
        }
    }
}
