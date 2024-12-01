using AutoMapper;

namespace FufelMarketBackend.Mapper;

public interface IMapTo<T>
{
    void Mapping(Profile profile) => profile.CreateMap(GetType(), typeof(T));
}