using Core.Base;
using Core.Base.Logger;
using Core.Base.Map;
using UnityEngine;
using VContainer;

namespace Core.Zones.Base.Id
{
    public class IdContext : IIdContext
    {
        [Inject] private readonly ILogger m_Logger;
        [Inject] private readonly MapBase<string, IIdStrategy> m_Strategies;

        public IdContext()
        {
            m_Logger = m_Logger.WithPrefix(nameof(IdContext));
        }

        public Result<string> GetId(GameObject zoneObject)
        {
            foreach (var (id, strategy) in m_Strategies.GetValues())
            {
                if (strategy.Contains(zoneObject))
                {
                    return Result<string>.Successful(id);
                }
            }

            m_Logger.Log($"Unknown zoneObject<{zoneObject.ToString()}>");
            return Result<string>.Invalid();
        }
    }
}