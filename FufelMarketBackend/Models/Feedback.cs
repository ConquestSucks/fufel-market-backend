using System;
using System.Collections.Generic;

namespace FufelMarketBackend.Models;

public partial class Feedback
{
    public int Id { get; set; }

    public int AuthorId { get; set; }

    public int AdOwnerId { get; set; }

    public int AdId { get; set; }

    public double Score { get; set; }

    public string? Text { get; set; }

    public virtual Ad Ad { get; set; } = null!;

    public virtual User AdOwner { get; set; } = null!;

    public virtual User Author { get; set; } = null!;
}
