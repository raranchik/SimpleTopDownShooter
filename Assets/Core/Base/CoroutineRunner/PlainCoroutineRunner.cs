using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Base.CoroutineRunner
{
    public class PlainCoroutineRunner : ICoroutineRunner, IDisposable
    {
        private readonly MonoCoroutineRunner m_Mono;

        public PlainCoroutineRunner()
        {
            var monoInstance = new GameObject(nameof(MonoCoroutineRunner));
            var monoRunner = monoInstance.AddComponent<MonoCoroutineRunner>();
            Object.DontDestroyOnLoad(monoInstance);
            m_Mono = monoRunner;
        }

        public Coroutine StartCoroutine(IEnumerator routine)
        {
            return m_Mono.StartCoroutine(routine);
        }

        public void StopCoroutine(Coroutine routine)
        {
            m_Mono.StopCoroutine(routine);
        }

        public void Dispose()
        {
            Object.Destroy(m_Mono);
        }
    }
}