using System.Collections;
using Core.Base.VContainerExt.Tests.T1;
using UnityEngine;
using UnityEngine.TestTools;
using VContainer;
using VContainer.Unity;

namespace Core.Base.VContainerExt.Tests
{
    public class VContainerInitializerTests
    {
        [UnityTest]
        public IEnumerator TestVContainerInitializationT1()
        {
            var name = nameof(TestVContainerInitializationT1);
            var type = typeof(TestLifetimeScopeContextT1);
            var scopeObject = new GameObject(name, type);
            var scope = scopeObject.GetComponent<LifetimeScope>();
            var resolver = scope.Container;
            var service = resolver.Resolve<ContextT1Service>();
            yield return null;
        }
    }
}