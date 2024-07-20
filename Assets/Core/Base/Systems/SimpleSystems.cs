using System.Collections.Generic;

namespace Core.Base.Systems
{
    public class SimpleSystems : ISystems
    {
        private readonly HashSet<ISystem> m_Systems = new HashSet<ISystem>();

        public void Attach(ISystem system)
        {
            m_Systems.Add(system);
        }

        public void Detach(ISystem system)
        {
            m_Systems.Remove(system);
        }

        public void Run()
        {
            foreach (var system in m_Systems)
            {
                system.Run();
            }
        }
    }
}