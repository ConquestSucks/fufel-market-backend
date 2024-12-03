using System.Net;
using FufelMarketBackend.AdvertisementCQ.Commands;
using FufelMarketBackend.CityCQ.Commands;
using FufelMarketBackend.CityCQ.Queries;
using FufelMarketBackend.Data;
using FufelMarketBackend.Vms;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FufelMarketBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController(AppDbContext context, ISender mediator) : ControllerBase
    {
        [HttpGet("getCities")]
        public async Task<List<CityVm>> GetCities()
        {
            return await mediator.Send(new GetCitiesQuery());
        }

        [HttpGet("getCity/{id:int}")]
        public async Task<CityVm> GetCity(int id)
        {
            return await mediator.Send(new GetCityByIdQuery
            {
                Id = id
            });
        }

        [HttpPost("postCity")]
        public async Task<HttpStatusCode> PostCity(PostCityCmd cmd)
        {
            return await mediator.Send(cmd);
        }
    }
}
