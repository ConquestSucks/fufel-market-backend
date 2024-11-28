using System.ComponentModel.DataAnnotations;

namespace FufelMarketBackend.Models;

public sealed class Advertisement
{
    public int Id { get; set; }
    
    public int UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool Published { get; set; }

    [StringLength(40)]
    public required string Title { get; set; }

    [StringLength(500)]
    public required string Description { get; set; }

    //public string Image { get; set; }

    public ulong Price { get; set; }
    
    //public required string City { get; set; } TODO: отдельная таблица

    //public string Category { get; set; } = null!; TODO: отдельная таблица
    
    public required User User { get; set; }

    public ICollection<Feedback> Feedbacks { get; init; } = new List<Feedback>();
}
