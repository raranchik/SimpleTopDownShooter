using Core.Base;
using Core.Base.Map;
using UnityEngine;
using VContainer;

namespace Core.Zones.Base.Generator
{
    public class GeneratorContext : IGeneratorContext
    {
        [Inject] private readonly ILogger m_Logger;
        [Inject] private readonly MapBase<string, IGeneratorStrategy> m_Strategies;

        public Result<IZone> GenerateZone(string id)
        {
            if (!m_Strategies.Contains(id))
            {
                if (!m_Strategies.Contains(id))
                {
                    m_Logger.Log($"Unknown strategy id<{id}>");
                    return Result<IZone>.Invalid();
                }
            }

            var strategy = m_Strategies.GetValue(id);
            var zone = strategy.GenerateZone();
            return zone;
        }
    }
}