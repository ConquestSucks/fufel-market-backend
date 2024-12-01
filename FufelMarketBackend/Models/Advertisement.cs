using System.ComponentModel.DataAnnotations;

namespace FufelMarketBackend.Models;

public sealed class Advertisement
{
    public int Id { get; set; }

    public required int UserId { get; set; }
    
    public required int CityId { get; set; }

    public required int CategoryId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; } = null;

    public bool Published { get; set; } = false;

    [StringLength(40)]
    public required string Title { get; set; }

    [StringLength(500)]
    public required string Description { get; set; }

    //public string Image { get; set; }

    public ulong Price { get; set; }
    
    public City? City { get; set; }

    public Category? Category { get; set; } 

    public User? User { get; set; }

    public ICollection<Feedback> Feedbacks { get; init; } = [];
}
