using System.Net;
using AutoMapper;
using FufelMarketBackend.Data;
using FufelMarketBackend.Mapper;
using FufelMarketBackend.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FufelMarketBackend.CityCQ.Commands;

public class PostCityCmd : IRequest<HttpStatusCode>, IMapTo<City>
{
    public required string Name { get; set; }
}

public class PostCityCmdHandler(AppDbContext context, IMapper mapper) : IRequestHandler<PostCityCmd, HttpStatusCode>
{
    public async Task<HttpStatusCode> Handle(PostCityCmd request, CancellationToken cancellationToken)
    {
        var cityExists = await context.Citys
            .AnyAsync(x => x.Name == request.Name, cancellationToken: cancellationToken);
        
        
        if (cityExists) {
            return HttpStatusCode.BadRequest;
        }
        
        var city = mapper.Map<City>(request);

        await context.Citys.AddAsync(city, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return HttpStatusCode.OK;
    }
}