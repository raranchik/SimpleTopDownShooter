using System.Collections.Generic;
using Core.Base.Map;

namespace Core.Zones.Base.Radius
{
    public class RadiusStrategiesMap : MapBase<string, IRadiusStrategy>
    {
        public RadiusStrategiesMap(IReadOnlyList<IRadiusStrategy> strategies)
        {
            foreach (var strategy in strategies)
            {
                Add(strategy.Id, strategy);
            }
        }
    }
}