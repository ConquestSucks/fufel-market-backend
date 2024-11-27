using System;
using System.Collections.Generic;

namespace FufelMarketBackend.Models;

public partial class User
{
    public int Id { get; set; }

    public string Role { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public virtual ICollection<Ad> Ads { get; set; } = new List<Ad>();

    public virtual ICollection<Feedback> FeedbackAdOwners { get; set; } = new List<Feedback>();

    public virtual ICollection<Feedback> FeedbackAuthors { get; set; } = new List<Feedback>();
}
