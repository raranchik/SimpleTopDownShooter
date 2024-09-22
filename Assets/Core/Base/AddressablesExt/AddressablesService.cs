using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core.Base.AddressablesExt
{
    public class AddressablesService
    {
        public async UniTask<bool> NeedToPreloadAsync(IKeyEvaluator address)
        {
            var downloadSizeOperation = Addressables.GetDownloadSizeAsync(address)
                .ToUniTask();
            await downloadSizeOperation;

            var needToPreload = downloadSizeOperation.Status == UniTaskStatus.Succeeded;
            Addressables.Release(downloadSizeOperation);

            return needToPreload;
        }

        public async UniTask<bool> PreloadAsync(IKeyEvaluator address)
        {
            var needToPreload = await NeedToPreloadAsync(address);
            if (!needToPreload)
            {
                return true;
            }

            var downloadHandle = Addressables.DownloadDependenciesAsync(address)
                .ToUniTask();
            await downloadHandle;

            var downloadHandleStatus = downloadHandle.Status;
            Addressables.Release(downloadHandle);

            return downloadHandleStatus == UniTaskStatus.Succeeded;
        }

        public async UniTask<T> LoadAsync<T>(IKeyEvaluator address) where T : Object
        {
            var handle = Addressables.LoadAssetAsync<T>(address)
                .ToUniTask();
            var result = await handle;

            return result;
        }

        public T LoadSync<T>(IKeyEvaluator assetId) where T : Object
        {
            var waitForCompletion = Addressables.LoadAssetAsync<T>(assetId)
                .WaitForCompletion();
            return waitForCompletion;
        }

        public void Unload(Object asset)
        {
            Addressables.Release(asset);
        }
    }
}