using Core.Base.VContainerExt.Attributes;
using UnityEngine;
using VContainer.Unity;

namespace Core.Base.VContainerExt.Tests.T1
{
    [RegisterEntryPoint, Context(typeof(ContextT1)), AsSelf]
    public class EntryPointT2 : IInitializable
    {
        public void Initialize()
        {
            Debug.Log($"Initialize {nameof(EntryPointT2)}");
        }
    }
}