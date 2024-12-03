using System.Windows.Input;
using AutoMapper;
using FufelMarketBackend.Data;
using FufelMarketBackend.Mapper;
using FufelMarketBackend.Models;
using FufelMarketBackend.Vms;
using MediatR;

namespace FufelMarketBackend.AdvertisementCQ.Commands;

public class PostAdCmd : IRequest<int>, IMapTo<Advertisement>
{
    public required int UserId { get; set; }

    public required int CategoryId { get; set; }

    public required int CityId { get; set; }
    
    public required string Title { get; set; }
    
    public required string Description { get; set; }

    public ulong Price { get; set; }
}

public class PostAdCmdHandler(AppDbContext context, IMapper mapper) : IRequestHandler<PostAdCmd, int>
{
    public async Task<int> Handle(PostAdCmd request, CancellationToken cancellationToken)
    {
        var advertisement = mapper.Map<Advertisement>(request);

        await context.Ads.AddAsync(advertisement, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return advertisement.Id;
    }
}