using System.ComponentModel.DataAnnotations.Schema;

namespace FufelMarketBackend.Models
{
    public sealed class Category
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required ICollection<SubCategory>? SubCategories { get; set; }
    }
}
