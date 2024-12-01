using AutoMapper;
using AutoMapper.QueryableExtensions;
using FufelMarketBackend.Data;
using FufelMarketBackend.Vms;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FufelMarketBackend.Queries;

public class GetUsersQuery : IRequest<List<UserVm>>;

public class GetUsersQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetUsersQuery, List<UserVm>>
{
    public async Task<List<UserVm>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await context.Users
            .AsNoTracking()
            .ProjectTo<UserVm>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
