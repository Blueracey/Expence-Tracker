using Microsoft.AspNetCore.Authentication.JwtBearer;
using expenceTracker.Data;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using expenceTracker.Services;

var key = Encoding.ASCII.GetBytes("07cbc59d859b81dab168abb802cf4dde44d5f62caae6a7d1497c58b1fc78b8a79a21d238adf2ccc5d871d37ffb9cf11ccb33eb09b4c1092c0115f7c1054b565c770510a0f0da9ad40b6cec900a07775b5118b8a4b8ade3d4bb0d03e5f48678aa");
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<AppDatabaseContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
    JwtBearerDefaults.AuthenticationScheme;

    options.DefaultAuthenticateScheme =
    JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddSingleton(new tokenService("07cbc59d859b81dab168abb802cf4dde44d5f62caae6a7d1497c58b1fc78b8a79a21d238adf2ccc5d871d37ffb9cf11ccb33eb09b4c1092c0115f7c1054b565c770510a0f0da9ad40b6cec900a07775b5118b8a4b8ade3d4bb0d03e5f48678aa"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}






app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=account}/{action=Index}/{id?}");

app.Run();
