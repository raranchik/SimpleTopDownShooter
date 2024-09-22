using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Core.Base.AddressablesExt
{
    public class AddressableInfo<T> : IAddressableInfo
    {
        private string m_Key;
        private IKeyEvaluator m_Evaluator;
        private UniTask<T> m_Task;
        private T m_Result;
        private bool m_IsLoaded;
        private bool m_IsFailed;
        private bool m_MarkToUnload;
        private float m_UsedLastTime;
        private bool m_IsPreloaded;

        public AddressableInfo(string key, IKeyEvaluator evaluator)
        {
            m_Key = key;
            m_Evaluator = evaluator;
        }

        public string Key => m_Key;
        public IKeyEvaluator Evaluator => m_Evaluator;

        public UniTask<T> Task
        {
            get => m_Task;
            set => m_Task = value;
        }

        public T Result
        {
            get => m_Result;
            set => m_Result = value;
        }

        public bool IsLoaded
        {
            get => m_IsLoaded;
            set => m_IsLoaded = value;
        }

        public bool MarkToUnload
        {
            get => m_MarkToUnload;
            set => m_MarkToUnload = value;
        }

        public float UsedLastTime
        {
            get => m_UsedLastTime;
            set => m_UsedLastTime = value;
        }

        public bool IsPreloaded
        {
            get => m_IsPreloaded;
            set => m_IsPreloaded = value;
        }

        public bool IsFailed
        {
            get => m_IsFailed;
            set => m_IsFailed = value;
        }
    }
}