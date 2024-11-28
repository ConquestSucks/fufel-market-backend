using DevOne.Security.Cryptography.BCrypt;
using FufelMarketBackend.Data;
using FufelMarketBackend.Models;
using FufelMarketBackend.Vms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FufelMarketBackend.Controllers
{
    [Route("/api/[controller]/")]
    [ApiController]
    public class UserController(AppDbContext context) : ControllerBase
    {
        [HttpGet("getUsers")]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await context.Users.ToListAsync();
        }

        [HttpGet("getUser/{id:int}")]
        public async Task<User?> GetUser(int id)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        [HttpPost("signUp")]
        public async Task<ActionResult> SignUp([FromBody] UserVm userVm)
        {
            var passwordHash = BCryptHelper.HashPassword(userVm.Password, BCryptHelper.GenerateSalt());
            if (string.IsNullOrWhiteSpace(passwordHash))
                return BadRequest("Пароль не указан");

            if (string.IsNullOrWhiteSpace(userVm.Email))
                return BadRequest("Почта не указана");
            
            if (string.IsNullOrWhiteSpace(userVm.FirstName))
                return BadRequest("Имя не указано");
            
            if (string.IsNullOrWhiteSpace(userVm.LastName))
                return BadRequest("Фамилия не указана");
            
            var user = new User
            {
                FirstName = userVm.FirstName,
                LastName = userVm.LastName,
                Email = userVm.Email,
                PasswordHash = passwordHash
            };

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("signIn")]
        public async Task<ActionResult> SignIn([FromQuery] string email, [FromQuery] string password)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user is null)
                return BadRequest("Неверный логин или пароль");
            
            var isPasswordCorrect = BCryptHelper.CheckPassword(password, user.PasswordHash);
            
            return isPasswordCorrect ? Ok("Вы успешно авторизовались") : BadRequest("Неверный логин или пароль");
        }
    }
}
