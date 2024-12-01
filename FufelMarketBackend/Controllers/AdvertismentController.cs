using AutoMapper;
using FufelMarketBackend.Data;
using FufelMarketBackend.Models;
using FufelMarketBackend.Vms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FufelMarketBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertismentController(AppDbContext context, IMapper mapper) : ControllerBase
    {
        [HttpGet("getAds")]
        public async Task<IEnumerable<Advertisement>> GetAds()
        {
            return await context.Ads.ToListAsync();
        }

        [HttpGet("getAdsByCityId/{cityId:int}")]
        public async Task<IEnumerable<Advertisement>?> GetAdsByCity(int cityId)
        {
            return await context.Ads.Where(ad => ad.CityId.Equals(cityId)).ToListAsync();
        }

        [HttpGet("getAd/{id:int}")]
        public async Task<AdvertisementVm?> GetAd(int id)
        {
            var advertisement = await context.Ads.FirstOrDefaultAsync(ad => ad.Id == id);
            
            return mapper.Map<AdvertisementVm>(advertisement);
        }

        [HttpPost("postAdd")]
        public async Task<ActionResult> PostAd([FromBody] AdvertisementVm advertisementVm)
        {
            var advertisement = mapper.Map<Advertisement>(advertisementVm);

            await context.Ads.AddAsync(advertisement);
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
