using System.Collections.Generic;
using Core.Base.Map;

namespace Core.Zones.Base.Id
{
    public class IdStrategiesMap : MapBase<string, IIdStrategy>
    {
        public IdStrategiesMap(IReadOnlyList<IIdStrategy> strategies)
        {
            foreach (var strategy in strategies)
            {
                Add(strategy.Id, strategy);
            }
        }
    }
}