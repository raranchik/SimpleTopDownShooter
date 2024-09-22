namespace Core.Zones.Base.Radius
{
    public interface IRadiusStrategy
    {
        string Id { get; }
        float GetRadius();
    }
}