using AutoMapper;
using AutoMapper.QueryableExtensions;
using FufelMarketBackend.Data;
using FufelMarketBackend.Models;
using FufelMarketBackend.Vms;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FufelMarketBackend.UserCQ.Queries;
public class GetUserQuery : IRequest<UserVm>
{
    public int Id { get; set; }
}

public class GetUserQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetUserQuery, UserVm?>
{
    public async Task<UserVm?> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        return await context.Users
            .AsNoTracking()
            .ProjectTo<UserVm>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);
    }
}