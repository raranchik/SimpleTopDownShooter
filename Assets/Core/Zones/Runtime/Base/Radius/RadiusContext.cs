using Core.Base;
using Core.Base.Logger;
using Core.Base.Map;
using UnityEngine;
using VContainer;

namespace Core.Zones.Base.Radius
{
    public class RadiusContext : IRadiusContext
    {
        [Inject] private readonly ILogger m_Logger;
        [Inject] private readonly MapBase<string, IRadiusStrategy> m_Strategies;

        public RadiusContext()
        {
            m_Logger = m_Logger.WithPrefix(nameof(RadiusContext));
        }

        public Result<float> GetRadius(string id)
        {
            if (!m_Strategies.Contains(id))
            {
                if (!m_Strategies.Contains(id))
                {
                    m_Logger.Log($"Unknown strategy id<{id}>");
                    return Result<float>.Invalid();
                }
            }

            var strategy = m_Strategies.GetValue(id);
            var radius = strategy.GetRadius();
            return Result<float>.Successful(radius);
        }
    }
}