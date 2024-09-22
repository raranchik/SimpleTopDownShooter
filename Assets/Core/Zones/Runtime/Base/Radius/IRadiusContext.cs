using Core.Base;

namespace Core.Zones.Base.Radius
{
    public interface IRadiusContext
    {
        Result<float> GetRadius(string id);
    }
}