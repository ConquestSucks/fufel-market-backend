using System.ComponentModel.DataAnnotations;

namespace FufelMarketBackend.Models;

public sealed class User
{
    public int Id { get; set; }
    
    [StringLength(15)]
    public required string FirstName { get; set; }
    
    [StringLength(15)]
    public required string LastName { get; set; }
    
    public string FullName => $"{FirstName} {LastName}";
    
    [StringLength(40)]
    public required string Email { get; set; }
    
    [StringLength(100)]
    public required string PasswordHash { get; set; }

    [StringLength(12)]
    public string PhoneNumber { get; set; } = string.Empty;

    public ICollection<Advertisement> Advertisements { get; set;} = new List<Advertisement>();
}
