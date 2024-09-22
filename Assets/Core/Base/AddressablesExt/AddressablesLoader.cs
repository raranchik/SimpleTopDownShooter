using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Object = UnityEngine.Object;

namespace Core.Base.AddressablesExt
{
    public class AddressablesLoader
    {
        private readonly AddressablesService m_AddressablesService = new AddressablesService();
        private readonly Dictionary<string, IAddressableInfo> m_Cache = new Dictionary<string, IAddressableInfo>();
        private readonly List<ILoadOperation> m_LoadOperations = new List<ILoadOperation>();
        private bool m_IsLoading;

        public bool IsLoading => m_IsLoading;

        public IAddressableInfo GetAddressableInfoFromCache(string key)
        {
            return m_Cache.GetValueOrDefault(key);
        }

        public AddressableInfo<T> GetOrLoadAddressableAsset<T>(string key) where T : Object
        {
            AddressableInfo<T> addressableInfo;
            var addressableInfoFromCache = GetAddressableInfoFromCache(key);
            if (addressableInfoFromCache == null)
            {
                addressableInfo = new AddressableInfo<T>(key, new StringKeyEvaluator(key));
                AddDownloadOperation(key, new AddressablesLoadOperation<T>(addressableInfo));
            }
            else
            {
                addressableInfo = addressableInfoFromCache as AddressableInfo<T>;
            }

            return addressableInfo;
        }

        public void AddDownloadOperation(string key, ILoadOperation loadOperation)
        {
            if (m_IsLoading)
            {
                return;
            }

            if (m_Cache.ContainsKey(key))
            {
                return;
            }

            m_LoadOperations.Add(loadOperation);
            m_Cache.Add(key, loadOperation.AddressableInfo);
        }

        public void LoadAll()
        {
            m_IsLoading = true;
            foreach (var loadOperation in m_LoadOperations)
            {
                loadOperation.Execute(m_AddressablesService);
            }
        }

        public IEnumerator CheckAllLoaded(Action<bool> onComplete)
        {
            if (m_LoadOperations.Count <= 0)
            {
                m_IsLoading = false;
                onComplete?.Invoke(true);
                yield break;
            }

            while (true)
            {
                var isAllLoaded = m_Cache.All(addressableInfo => addressableInfo.Value.IsLoaded);
                if (isAllLoaded)
                {
                    m_LoadOperations.Clear();
                    m_IsLoading = false;
                    onComplete?.Invoke(true);
                    yield break;
                }

                yield return null;
            }
        }

        public T LoadSync<T>(string key) where T : Object
        {
            if (m_IsLoading)
            {
                return null;
            }

            var addressableInfoFromCache = GetAddressableInfoFromCache(key);
            if (addressableInfoFromCache != null)
            {
                return (addressableInfoFromCache as AddressableInfo<T>)?.Result;
            }

            var obj = m_AddressablesService.LoadSync<T>(new StringKeyEvaluator(key));
            var addressableInfo = new AddressableInfo<T>(key, new StringKeyEvaluator(key));
            addressableInfo.IsLoaded = true;
            addressableInfo.Result = obj;
            m_Cache.Add(key, addressableInfo);
            return obj;
        }

        public void Unload<T>(string key) where T : Object
        {
            var addressableInfoFromCache = GetAddressableInfoFromCache(key);
            if (addressableInfoFromCache == null)
            {
                return;
            }

            var addressableInfo = addressableInfoFromCache as AddressableInfo<T>;
            var obj = addressableInfo.Result;
            var objectReference = obj as Object;
            if (objectReference == null)
            {
                return;
            }

            m_Cache.Remove(key);
            m_AddressablesService.Unload(objectReference);
        }
    }
}