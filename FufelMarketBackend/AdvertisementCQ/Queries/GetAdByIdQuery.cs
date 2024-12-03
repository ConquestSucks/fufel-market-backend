using AutoMapper;
using AutoMapper.QueryableExtensions;
using FufelMarketBackend.Data;
using FufelMarketBackend.Vms;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FufelMarketBackend.AdvertisementCQ.Queries;

public class GetAdByIdQuery : IRequest<AdvertisementVm>
{
    public int Id { get; set; }
}

public class GetAdByIdQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetAdByIdQuery, AdvertisementVm?>
{
    public async Task<AdvertisementVm?> Handle(GetAdByIdQuery query, CancellationToken cancellationToken)
    {
        return await context.Ads
            .AsNoTracking()
            .ProjectTo<AdvertisementVm>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(ad => ad.Id == query.Id, cancellationToken);
    }
}
