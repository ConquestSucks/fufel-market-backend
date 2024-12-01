using System.Reflection;
using AutoMapper;

namespace FufelMarketBackend.Mapper;

public class MapperProfile : Profile
{
    private readonly Assembly[] _assemblies;
    
    public MapperProfile()
    {
        _assemblies =
        [
            Assembly.Load("FufelMarketBackend")
        ];
        
        ApplyMappers();
    }

    private void ApplyMappers()
    {
        var mappers = GetMappers();
        
        foreach (var type in mappers)
        {
            var instance = Activator.CreateInstance(type);
            var methodInfo = type.GetMethod("Mapping")
                             ?? type.GetInterface("IMapFrom`1")?.GetMethod("Mapping")
                             ?? type.GetInterface("IMapTo`1")?.GetMethod("Mapping");
            methodInfo?.Invoke(instance, [this]);
        }
    }
    
    private List<Type> GetMappers()
    {
        var mappers = new List<Type>();
            
        foreach (var assembly in _assemblies)
        {
            mappers.AddRange(assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType &&
                    (i.GetGenericTypeDefinition() == typeof(IMapFrom<>) ||
                     i.GetGenericTypeDefinition() == typeof(IMapTo<>)
                    )
                ))
                .ToList());
        }

        return mappers;
    }
}