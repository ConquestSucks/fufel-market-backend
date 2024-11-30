using FufelMarketBackend.Data;
using FufelMarketBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FufelMarketBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController(AppDbContext context) : ControllerBase
    {
        [HttpGet("getCities")]
        public async Task<List<City>> GetCities()
        {
            return await context.Citys.ToListAsync();
        }

        [HttpGet("getCity/{id:int}")]
        public async Task<City> GetCity(int id)
        {
            return await context.Citys.FirstOrDefaultAsync(ad => ad.Id == id);
        }
    }
}
