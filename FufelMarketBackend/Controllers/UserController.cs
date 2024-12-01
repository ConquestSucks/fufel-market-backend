using FufelMarketBackend.Commands;
using FufelMarketBackend.Data;
using FufelMarketBackend.Models;
using FufelMarketBackend.Queries;
using FufelMarketBackend.Vms;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FufelMarketBackend.Controllers
{
    [Route("/api/[controller]/")]
    [ApiController]
    public class UserController(AppDbContext context, ISender mediator) : ControllerBase
    {
        [HttpGet("getUsers")]
        public async Task<List<UserVm>> GetUsers()
        {
            return await mediator.Send(new GetUsersQuery());
        }

        [HttpGet("getUser/{id:int}")]
        public async Task<User?> GetUser(int id)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        [HttpPost("signUp")]
        public async Task<int?> SignUp(SignUpCmd cmd)
        {
            return await mediator.Send(cmd);
        }

        [HttpPost("signIn")]
        public async Task<ActionResult> SignIn([FromQuery] string email, [FromQuery] string password)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user is null)
                return BadRequest("Неверный логин или пароль");
            
            var isPasswordCorrect = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            
            return isPasswordCorrect ? Ok("Вы успешно авторизовались") : BadRequest("Неверный логин или пароль");
        }
    }
}
