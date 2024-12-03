using FufelMarketBackend.Mapper;
using FufelMarketBackend.Models;

namespace FufelMarketBackend.Vms;

public class CityVm: IMapTo<CityVm>, IMapFrom<City>
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string? TimeZone { get; set; }
}