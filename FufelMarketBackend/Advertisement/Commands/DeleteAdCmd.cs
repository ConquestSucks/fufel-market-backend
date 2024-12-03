using System.Net;
using FufelMarketBackend.Data;
using FufelMarketBackend.Mapper;
using FufelMarketBackend.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace FufelMarketBackend.Commands;

public class DeleteAdCmd : IRequest<HttpStatusCode>, IMapTo<Advertisement>
{
    public int Id { get; set; }
}

public class DeleteAdCmdHandler(AppDbContext context) : IRequestHandler<DeleteAdCmd, HttpStatusCode>
{
    public async Task<HttpStatusCode> Handle(DeleteAdCmd request, CancellationToken cancellationToken)
    {
        var ad = await context.Ads
            .FirstOrDefaultAsync(ad => ad.Id == request.Id, cancellationToken: cancellationToken);
        if (ad is null)
        {
            return HttpStatusCode.BadRequest;
        }
        context.Ads.Remove(ad);
        await context.SaveChangesAsync(cancellationToken);

        return HttpStatusCode.OK;
    }
}