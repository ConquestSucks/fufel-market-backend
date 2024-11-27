using System;
using System.Collections.Generic;

namespace FufelMarketBackend.Models;

public partial class Ad
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool Published { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? Image { get; set; }

    public int? Price { get; set; }

    public string City { get; set; } = null!;

    public string Category { get; set; } = null!;

    public int UserId { get; set; }

    public int Views { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual User User { get; set; } = null!;
}
