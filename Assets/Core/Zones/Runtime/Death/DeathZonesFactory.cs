using Core.Base.Factory;
using UnityEngine;

namespace Core.Zones.Death
{
    public class DeathZonesFactory : IFactoryWithArgs<IDeathViewZone, DeathZoneFactoryArgs>
    {
        private readonly DeathZoneView m_Prefab;
        private readonly Vector3 m_Scale;

        public DeathZonesFactory(DeathZoneView prefab, DeathZonesConfig config)
        {
            m_Prefab = prefab;
            m_Scale = Vector3.one * config.Radius;
        }

        public IDeathViewZone Create(DeathZoneFactoryArgs args)
        {
            var instance = Object.Instantiate(m_Prefab);
            instance.SetPosition(args.Position);
            instance.SetScale(m_Scale);
            return instance;
        }
    }
}