using AutoMapper;
using AutoMapper.QueryableExtensions;
using FufelMarketBackend.Data;
using FufelMarketBackend.Vms;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FufelMarketBackend.AdvertisementCQ.Queries;

public class GetAdsQuery : IRequest<List<AdvertisementVm>>;
  
public class GetAdsQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetAdsQuery, List<AdvertisementVm>>
{
    public async Task<List<AdvertisementVm>> Handle(GetAdsQuery query, CancellationToken cancellationToken)
    {
        return await context.Ads
            .AsNoTracking()
            .ProjectTo<AdvertisementVm>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}

