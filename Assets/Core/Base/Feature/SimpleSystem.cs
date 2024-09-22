using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Base.Feature
{
    public class SimpleSystem : ISystem
    {
        private readonly Type m_Name;
        private readonly IEnumerable<Func<bool>> m_Predicates;
        private readonly Action m_OnInitialized;
        private readonly Dictionary<string, IFeature> m_Features = new Dictionary<string, IFeature>();
        private readonly List<IFeature> m_OrderedFeatures = new List<IFeature>();
        private readonly List<KeyValuePair<int, IFeature>> m_OrderedPairs = new List<KeyValuePair<int, IFeature>>();

        public SimpleSystem(Type name, IEnumerable<Func<bool>> predicates, Action onInitialized)
        {
            m_Name = name;
            m_Predicates = predicates;
            m_OnInitialized = onInitialized;
        }

        public Type GetName()
        {
            return m_Name;
        }

        public void AddFeature(int order, IFeature feature)
        {
            if (feature == null)
            {
                return;
            }

            var name = feature.GetType().AssemblyQualifiedName;
            if (!m_Features.TryAdd(name, feature))
            {
                return;
            }

            var pair = new KeyValuePair<int, IFeature>(order, feature);
            m_OrderedPairs.Add(pair);
        }

        public IEnumerable<IFeature> GetFeatures()
        {
            return m_OrderedFeatures;
        }

        public bool IsPredicatesCompleted()
        {
            return m_Predicates?.All(x => x.Invoke()) ?? true;
        }

        public void OnInitializeCompleted()
        {
            m_OnInitialized?.Invoke();
        }

        public void SortFeatures()
        {
            m_OrderedPairs.Sort((x, y) => x.Key.CompareTo(y.Key));
            m_OrderedFeatures.Clear();
            foreach (var pair in m_OrderedPairs)
            {
                m_OrderedFeatures.Add(pair.Value);
            }
        }
    }
}