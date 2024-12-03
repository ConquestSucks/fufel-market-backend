using System.Net;
using AutoMapper;
using FufelMarketBackend.Commands;
using FufelMarketBackend.Data;
using FufelMarketBackend.Models;
using FufelMarketBackend.Queries;
using FufelMarketBackend.Vms;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FufelMarketBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertismentController(AppDbContext context, ISender mediator) : ControllerBase
    {
        [HttpGet("getAds")]
        public async Task<List<AdvertisementVm>> GetAds()
        {
            return await mediator.Send(new GetAdsQuery());
        }

        [HttpGet("getAdsByCityId/{cityId:int}")]
        public async Task<IEnumerable<AdvertisementVm>> GetAdsByCity(int cityId)
        {
            return await mediator.Send(new GetAdsByCityIdQuery
            {
                CityId = cityId
            });
        }

        [HttpGet("getAd/{id:int}")]
        public async Task<AdvertisementVm?> GetAd(int id)
        {
            return await mediator.Send(new GetAdByIdQuery
            {
                Id = id
            });
        }

        [HttpPost("postAdd")]
        public async Task<int> PostAd(PostAdCmd cmd)
        {
            return await mediator.Send(cmd);
        }

        [HttpDelete("deleteAd/{id:int}")]
        public async Task<HttpStatusCode> DeleteAd(int id)
        {
            return await mediator.Send(new DeleteAdCmd()
            {
                Id = id
            });
        }
    }
}
