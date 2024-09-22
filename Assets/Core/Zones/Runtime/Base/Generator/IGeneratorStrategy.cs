using Core.Base;

namespace Core.Zones.Base.Generator
{
    public interface IGeneratorStrategy
    {
        string Id { get; }
        Result<IZone> GenerateZone();
    }
}