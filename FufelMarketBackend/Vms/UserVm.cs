using AutoMapper;
using FufelMarketBackend.Mapper;
using FufelMarketBackend.Models;

namespace FufelMarketBackend.Vms;

public class UserVm : IMapFrom<User>
{
    public int Id { get; set; }
    
    public required string Email { get; set; }
    
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
}