using AutoMapper;
using AutoMapper.QueryableExtensions;
using FufelMarketBackend.Data;
using FufelMarketBackend.Vms;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FufelMarketBackend.CityCQ.Queries;

public class GetCitiesQuery : IRequest<List<CityVm>>;

public class GetCitiesQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetCitiesQuery, List<CityVm>>
{
    public async Task<List<CityVm>> Handle(GetCitiesQuery query, CancellationToken cancellationToken)
    {
        return await context.Citys
            .AsNoTracking()
            .ProjectTo<CityVm>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}