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
        private readonly SortedDictionary<int, IFeature> m_Features = new SortedDictionary<int, IFeature>();

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
            m_Features.Add(order, feature);
        }

        public IEnumerable<IFeature> GetFeatures()
        {
            return m_Features?.Values.ToArray();
        }

        public bool IsPredicatesCompleted()
        {
            return m_Predicates?.All(x => x.Invoke()) ?? true;
        }

        public void OnInitializeCompleted()
        {
            m_OnInitialized?.Invoke();
        }
    }
}