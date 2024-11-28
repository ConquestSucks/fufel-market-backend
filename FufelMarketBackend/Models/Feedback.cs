using System.ComponentModel.DataAnnotations;

namespace FufelMarketBackend.Models;

public sealed class Feedback
{
    public int Id { get; set; }
    
    public int AuthorId { get; set; }
    
    public int AdvertisementId { get; set; }
    
    public int Score { get; set; }
    
    [StringLength(300)]
    public required string FeedbackText { get; set; }
    
    public required User Author { get; set; }
    
    public required Advertisement Advertisement { get; set; }
}
