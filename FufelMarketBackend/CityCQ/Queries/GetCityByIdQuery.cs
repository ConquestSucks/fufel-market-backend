using AutoMapper;
using AutoMapper.QueryableExtensions;
using FufelMarketBackend.Data;
using FufelMarketBackend.Mapper;
using FufelMarketBackend.Models;
using FufelMarketBackend.Vms;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FufelMarketBackend.CityCQ.Queries;

public class GetCityByIdQuery : IRequest<CityVm>, IMapTo<CityVm>, IMapFrom<City>
{
    public int Id { get; set; }
}

public class GetCityByIdQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetCityByIdQuery, CityVm?>
{
    public async Task<CityVm?> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
    {
        return await context.Citys
            .AsNoTracking()
            .ProjectTo<CityVm>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(ad => ad.Id == request.Id, cancellationToken);
    }
}