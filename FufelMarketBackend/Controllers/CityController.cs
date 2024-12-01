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

        [HttpPost("addCity")]
        public async Task<ActionResult> PostCity(string cityName) { 
            var cities = await context.Citys.ToListAsync();

            if (cities.Any((c) => c.Name == cityName)) {
                return BadRequest("Такой город уже существует");
            }

            var city = new City
            {
                Name = cityName,
            };

            await context.Citys.AddAsync(city);
            await context.SaveChangesAsync();

            return Ok("Город успешно добавлен");
        }
        
    }
}
