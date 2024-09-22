using Core.Base;
using Core.Base.Factory;
using Core.Base.Logger;
using Core.Base.Map;
using Core.Zones.Base;
using Core.Zones.Base.Generator;
using Core.Zones.Base.Id;
using Core.Zones.Base.Radius;
using UnityEngine;
using VContainer;

namespace Core.Zones.Death
{
    public class DeathZoneStrategy : IIdStrategy, IGeneratorStrategy, IRadiusStrategy
    {
        [Inject] private readonly ILogger m_Logger;
        [Inject] private readonly IZonesService m_ZonesService;
        [Inject] private readonly MapBase<GameObject, IZone> m_AllZones;
        [Inject] private readonly MapBase<GameObject, IDeathViewZone> m_DeathZones;
        [Inject] private readonly IFactoryWithArgs<IDeathViewZone, DeathZoneFactoryArgs> m_ZonesFactory;
        [Inject] private readonly DeathZonesConfig m_ZonesConfig;

        private Bounds m_Bounds;

        public DeathZoneStrategy()
        {
            m_Logger = m_Logger.WithPrefix(nameof(DeathZoneStrategy));
        }

        public string Id => ZonesDefinition.DeathZoneId;

        public void Initialize()
        {
            var radius = m_ZonesConfig.Radius;
            m_Bounds = m_ZonesService.CalculateGenerateZoneBounds(radius);
        }

        public float GetRadius()
        {
            return m_ZonesConfig.Radius;
        }

        public bool Contains(GameObject zoneObject)
        {
            return m_DeathZones.Contains(zoneObject);
        }

        public Result<IZone> GenerateZone()
        {
            return Result<IZone>.Invalid();
        }

        private Result<Vector3> CalculateRandomZonePosition()
        {
            var iteration = 0;
            while (iteration < ZonesGeneratorDefinition.MaxGenerateIterations)
            {
                var position = m_ZonesService.CalculateRandomZonePosition(m_Bounds);
                var isZonePositionValid = m_ZonesService.IsZonePositionValid(position, m_ZonesConfig.Radius);
                if (isZonePositionValid)
                {
                    return Result<Vector3>.Successful(position);
                }

                iteration++;
            }

            m_Logger.Log($"Iterations count for position generation is exceeded : iterations<{iteration.ToString()}>");
            return Result<Vector3>.Invalid();
        }
    }
}