using AutoMapper;
using AutoMapper.QueryableExtensions;
using FufelMarketBackend.Data;
using FufelMarketBackend.Models;
using FufelMarketBackend.Vms;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FufelMarketBackend.AdvertisementCQ.Queries;
public class GetAdsByCityIdQuery : IRequest<List<AdvertisementVm>>
{
    public required int CityId { get; set; }
}
  
public class GetAdsByCityIdQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetAdsByCityIdQuery, List<AdvertisementVm>>
{
    public async Task<List<AdvertisementVm>> Handle(GetAdsByCityIdQuery query, CancellationToken cancellationToken)
    {
        return await context.Ads
            .AsNoTracking()
            .Where(ad => ad.CityId.Equals(query.CityId))
            .ProjectTo<AdvertisementVm>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}

