using FufelMarketBackend.Data;
using FufelMarketBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FufelMarketBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(AppDbContext context) : ControllerBase
    {
        [HttpGet("getCategories")]
        public async Task<List<Category>> GetAdvertisements()
        {
            return await context.Categories.ToListAsync();
        }

        [HttpGet("getCategory/{id:int}")]
        public async Task<Category> Get(int id)
        {
            return await context.Categories.FirstOrDefaultAsync(ad => ad.Id == id);
        }
    }
}
