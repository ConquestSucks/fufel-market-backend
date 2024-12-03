using FufelMarketBackend.Models;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using FufelMarketBackend.Mapper;

namespace FufelMarketBackend.Vms;

public class AdvertisementVm : IMapTo<Advertisement>, IMapFrom<Advertisement>
{
    public int Id { get; set; } 
    
    public required int UserId { get; set; }

    public required int CategoryId { get; set; }

    public required int CityId { get; set; }

    [StringLength(40)]
    public required string Title { get; set; }

    [StringLength(500)]
    public required string Description { get; set; }

    public ulong Price { get; set; }
}