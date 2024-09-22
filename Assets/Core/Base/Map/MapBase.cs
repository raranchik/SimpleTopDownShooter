using System.Collections.Generic;

namespace Core.Base.Map
{
    public abstract class MapBase<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> m_Map = new Dictionary<TKey, TValue>();

        public void Add(TKey key, TValue value)
        {
            m_Map.TryAdd(key, value);
        }

        public void Remove(TKey key)
        {
            m_Map.Remove(key);
        }

        public bool Contains(TKey key)
        {
            return m_Map.ContainsKey(key);
        }

        public TValue GetValue(TKey key)
        {
            return m_Map.GetValueOrDefault(key);
        }

        public Dictionary<TKey, TValue> GetValues()
        {
            return m_Map;
        }

        public bool HasValues()
        {
            return m_Map.Count > 0;
        }
    }
}