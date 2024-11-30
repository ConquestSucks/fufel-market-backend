using FufelMarketBackend.Data;
using FufelMarketBackend.Models;
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

        [HttpGet("getAd/{id:int}")]
        public async Task<Advertisement> GetAd(int id)
        {
            return await context.Ads.FirstOrDefaultAsync(ad => ad.Id == id);
        }
    }
}
