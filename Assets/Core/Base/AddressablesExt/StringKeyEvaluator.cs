using UnityEngine.AddressableAssets;

namespace Core.Base.AddressablesExt
{
    public class StringKeyEvaluator : IKeyEvaluator
    {
        private readonly string m_Key;

        public object RuntimeKey => m_Key;

        public StringKeyEvaluator(string key)
        {
            m_Key = key;
        }

        public bool RuntimeKeyIsValid()
        {
            return string.IsNullOrEmpty(m_Key);
        }
    }
}