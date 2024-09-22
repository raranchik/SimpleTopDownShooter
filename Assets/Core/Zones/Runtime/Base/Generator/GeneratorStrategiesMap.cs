using System.Collections.Generic;
using Core.Base.Map;

namespace Core.Zones.Base.Generator
{
    public class GeneratorStrategiesMap : MapBase<string, IGeneratorStrategy>
    {
        public GeneratorStrategiesMap(IReadOnlyList<IGeneratorStrategy> strategies)
        {
            foreach (var strategy in strategies)
            {
                Add(strategy.Id, strategy);
            }
        }
    }
}