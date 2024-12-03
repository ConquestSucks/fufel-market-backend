using System.Net;
using FufelMarketBackend.Data;
using FufelMarketBackend.UserCQ.Queries;
using FufelMarketBackend.UserCQ.Commands;
using FufelMarketBackend.Vms;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<UserVm?> GetUser(int id)
        {
            return await mediator.Send(new GetUserQuery
            {
                Id = id
            });
        }

        [HttpPost("signUp")]
        public async Task<int?> SignUp(SignUpCmd cmd)
        {
            return await mediator.Send(cmd);
        }

        [HttpPost("signIn")]
        public async Task<HttpStatusCode> SignIn(SignInCmd cmd)
        {
            return await mediator.Send(cmd);
        }
    }
}
