using Core.Base;
using Core.Zones.Base;
using UnityEngine;

namespace Core.Zones
{
    public interface IZonesService
    {
        Bounds CalculateGenerateZoneBounds(float zoneRadius);
        bool IsZonePositionValid(Vector3 lhsZonePosition, float lhsZoneRadius);
        Result<IZone> GenerateZone(string id);
        Result<float> GetZoneRadius(string id);
        Vector3 CalculateRandomZonePosition(Bounds bounds);
    }
}