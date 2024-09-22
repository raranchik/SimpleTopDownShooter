using Core.Base;

namespace Core.Zones.Base.Generator
{
    public interface IGeneratorContext
    {
        Result<IZone> GenerateZone(string id);
    }
}