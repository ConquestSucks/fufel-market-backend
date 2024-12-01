using FufelMarketBackend.Data;
using FufelMarketBackend.Models;
using FufelMarketBackend.Vms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FufelMarketBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertismentController(AppDbContext context) : ControllerBase
    {
        [HttpGet("getAds")]
        public async Task<IEnumerable<Advertisement>> GetAds()
        {
            return await context.Ads.ToListAsync();
        }

        [HttpGet("getAdsByCityId/{cityId}")]
        public async Task<IEnumerable<Advertisement>?> GetAdsByCity(int cityId)
        {
            return await context.Ads.Where(ad => ad.CityId.Equals(cityId)).ToListAsync();
        }

        [HttpGet("getAd/{id:int}")]
        public async Task<Advertisement?> GetAd(int id)
        {
            return await context.Ads.FirstOrDefaultAsync(ad => ad.Id == id);
        }

        [HttpPost("postAdd")]
        public async Task<ActionResult> PostAd([FromBody] AdvertisementVm advertisementVm)
        {
            var ad = new Advertisement
            {
                Title = advertisementVm.Title,
                Description = advertisementVm.Description,
                Price = advertisementVm.Price,
                CityId = advertisementVm.CityId,
                CategoryId = advertisementVm.CategoryId,
                UserId = advertisementVm.UserId
            };

            await context.Ads.AddAsync(ad);
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
