using UnityEngine;

namespace Core.Zones.Base.Id
{
    public interface IIdStrategy
    {
        string Id { get; }
        bool Contains(GameObject zoneObject);
    }
}