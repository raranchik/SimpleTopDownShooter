using Core.Base;
using Core.Base.Map;
using Core.Base.RandomExt;
using Core.Field.Runtime;
using Core.Zones.Base;
using Core.Zones.Base.Generator;
using Core.Zones.Base.Id;
using Core.Zones.Base.Radius;
using UnityEngine;
using VContainer;

namespace Core.Zones
{
    public class ZonesService : IZonesService
    {
        [Inject] private IFieldService m_FieldService;
        [Inject] private IRandom m_Random;
        [Inject] private IIdContext m_Id;
        [Inject] private IGeneratorContext m_Generator;
        [Inject] private IRadiusContext m_Radius;
        [Inject] private MapBase<GameObject, IZone> m_AllZones;

        private Vector3 m_HalfFieldSize;
        private float m_ZonesSpacing;
        private float m_FieldMargin;

        public Result<IZone> GenerateZone(string id)
        {
            return m_Generator.GenerateZone(id);
        }

        public Result<string> GetZoneIdByGameObject(GameObject zoneObject)
        {
            return m_Id.GetId(zoneObject);
        }

        public Result<float> GetZoneRadius(string id)
        {
            return m_Radius.GetRadius(id);
        }

        public Bounds CalculateGenerateZoneBounds(float zoneRadius)
        {
            var min = new Vector3()
            {
                x = -m_HalfFieldSize.x + zoneRadius + m_FieldMargin,
                y = -m_HalfFieldSize.y + zoneRadius + m_FieldMargin
            };
            var max = new Vector3()
            {
                x = m_HalfFieldSize.x - zoneRadius - m_FieldMargin,
                y = m_HalfFieldSize.y - zoneRadius - m_FieldMargin
            };
            var bounds = new Bounds()
            {
                min = min,
                max = max
            };

            return bounds;
        }

        public Vector3 CalculateRandomZonePosition(Bounds bounds)
        {
            var position = new Vector3(m_Random.NextFloat(), m_Random.NextFloat());
            var min = bounds.min;
            var max = bounds.max;
            position.x = position.x * (max.x - min.x) + min.x;
            position.y = position.y * (max.y - min.y) + min.y;
            return position;
        }

        public bool IsZonePositionValid(Vector3 lhsZonePosition, float lhsZoneRadius)
        {
            foreach (var (zoneObject, zone) in m_AllZones.GetValues())
            {
                var rhsZoneId = GetZoneIdByGameObject(zoneObject);
                if (!rhsZoneId.IsValid())
                {
                    continue;
                }

                var rhsZoneRadius = GetZoneRadius(rhsZoneId.Value);
                if (!rhsZoneRadius.IsValid())
                {
                    continue;
                }

                var rhsZonePosition = zone.Position;
                var distance = (lhsZonePosition - rhsZonePosition).magnitude;
                if (distance < lhsZoneRadius + rhsZoneRadius.Value + m_ZonesSpacing)
                {
                    return false;
                }
            }

            return true;
        }
    }
}