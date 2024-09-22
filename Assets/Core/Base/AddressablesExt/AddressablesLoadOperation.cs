using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Base.AddressablesExt
{
    public class AddressablesLoadOperation<T> : ILoadOperation where T : Object
    {
        private AddressableInfo<T> m_AddressableInfo;

        public AddressablesLoadOperation(AddressableInfo<T> addressableInfo)
        {
            m_AddressableInfo = addressableInfo;
        }

        public IAddressableInfo AddressableInfo => m_AddressableInfo;

        public bool IsLoaded => m_AddressableInfo.IsLoaded;

        public bool IsFailed => m_AddressableInfo.IsFailed;

        public async UniTaskVoid Execute(AddressablesService addressablesService)
        {
            T result = await addressablesService.LoadAsync<T>(m_AddressableInfo.Evaluator);
            m_AddressableInfo.Result = result;
            m_AddressableInfo.IsLoaded = true;
            m_AddressableInfo.UsedLastTime = Time.realtimeSinceStartup;
        }
    }
}