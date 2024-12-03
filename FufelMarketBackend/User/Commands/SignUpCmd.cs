using AutoMapper;
using FufelMarketBackend.Data;
using FufelMarketBackend.Mapper;
using FufelMarketBackend.Models;
using MediatR;

namespace FufelMarketBackend.Commands;

public class SignUpCmd : IRequest<int?>, IMapTo<User>
{
    public required string Email { get; set; }
    
    public required string Password { get; set; }
    
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
}

public class SignUpCmdHandler(AppDbContext context, IMapper mapper) : IRequestHandler<SignUpCmd, int?>
{
    public async Task<int?> Handle(SignUpCmd request, CancellationToken cancellationToken)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password, BCrypt.Net.BCrypt.GenerateSalt());
        if (string.IsNullOrWhiteSpace(passwordHash))
            return null;

        if (string.IsNullOrWhiteSpace(request.Email))
            return null;

        if (string.IsNullOrWhiteSpace(request.FirstName))
            return null;

        if (string.IsNullOrWhiteSpace(request.LastName))
            return null;

        var user = mapper.Map<User>(request);

        user.PasswordHash = passwordHash;

        await context.Users.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}