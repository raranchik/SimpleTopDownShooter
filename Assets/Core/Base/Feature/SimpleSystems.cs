using System;
using System.Collections.Generic;
using System.Reflection;
using Core.Base.Feature.Attributes;

namespace Core.Base.Feature
{
    public class SimpleSystems
    {
        private Dictionary<Type, IFeature> m_FeaturesMap = new Dictionary<Type, IFeature>();

        public void Initialize(IEnumerable<IFeature> features)
        {
            foreach (var feature in features)
            {
                var type = feature.GetType();
                m_FeaturesMap.Add(type, feature);
            }
        }

        public void SetByAttributes(IRunner runner)
        {
            foreach (var feature in m_FeaturesMap)
            {
                var attribute = feature.Key.GetCustomAttribute<SystemAttribute>();
                if (attribute == null)
                {
                    continue;
                }

                var system = runner.GetSystem(attribute.Name);
                if (system == null)
                {
                    continue;
                }
                
                system.AddFeature(attribute.Order, feature.Value);
            }
        }

        public IFeature GetFeature(Type name)
        {
            return m_FeaturesMap[name];
        }
    }
}