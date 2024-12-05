using expenceTracker.Data;
using expenceTracker.Models;
using expenceTracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;

//https://localhost:7267/account/Register
namespace expenceTracker.Controllers
{
    
    public class accountController : Controller
    {
        private readonly AppDatabaseContext _context;
        private readonly tokenService _tokenService; 
        public accountController(AppDatabaseContext context, tokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }
        
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(userRegisterDTO dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),

            };
            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return Ok("user registered successfully");
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDTO dto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Name == dto.Name);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password,user.Password)) 
                return Unauthorized("Invalid Credentials");

            var token = _tokenService.GenerateToken(user.Name);


            Response.Cookies.Append("AuthToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(30) //expires in 30 minutes
            });



            return Ok(new {Token  = token});
        }





    }
}
