using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Core.Base.Feature
{
    public class SimpleRunner : IRunner
    {
        private readonly ILauncher m_Launcher;
        private readonly Dictionary<Type, ISystem> m_Systems = new Dictionary<Type, ISystem>();

        public SimpleRunner(ILauncher launcher)
        {
            m_Launcher = launcher;
        }

        public IRunner AddSystem(ISystem system)
        {
            m_Systems.Add(system.GetName(), system);
            return this;
        }

        public IEnumerator Initialize()
        {
            yield return m_Launcher.InitializeSystems(m_Systems.Values.ToArray());
        }

        public ISystem GetSystem(Type name)
        {
            return m_Systems[name];
        }

        public void SortFeaturesOnEachSystem()
        {
            foreach (var (_, system) in m_Systems)
            {
                system.SortFeatures();
            }
        }
    }
}