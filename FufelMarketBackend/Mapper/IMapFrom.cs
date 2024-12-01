using AutoMapper;

namespace FufelMarketBackend.Mapper;

public interface IMapFrom<T>
{
    void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
}