using System;
using System.Collections.Generic;

namespace Core.Base.Notifier
{
    public class Notifier<T> : INotifier<T>
    {
        private readonly HashSet<T> m_Observers = new HashSet<T>();

        public void Attach(T observer)
        {
            m_Observers.Add(observer);
        }

        public void Detach(T observer)
        {
            m_Observers.Remove(observer);
        }

        public void NotifyAll(Action<T> action)
        {
            foreach (var observer in m_Observers)
            {
                action?.Invoke(observer);
            }
        }
    }
}