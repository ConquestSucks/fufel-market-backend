using System.Net;
using FufelMarketBackend.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FufelMarketBackend.UserCQ.Commands;

public class SignInCmd : IRequest<HttpStatusCode>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class SignInCmdHandler(AppDbContext context) : IRequestHandler<SignInCmd, HttpStatusCode>
{
    public async Task<HttpStatusCode> Handle(SignInCmd request, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken: cancellationToken);
        if (user is null)
            return HttpStatusCode.BadRequest;
            
        var isPasswordCorrect = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            
        return isPasswordCorrect ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
    }
}