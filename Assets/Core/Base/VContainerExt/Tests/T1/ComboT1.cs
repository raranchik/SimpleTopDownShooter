using Core.Base.VContainerExt.Attributes;
using UnityEngine;
using VContainer.Unity;

namespace Core.Base.VContainerExt.Tests.T1
{
    [RegisterEntryPoint, Context(typeof(ContextT1)), As(typeof(IAsT1), typeof(IAsT2)), AsSelf]
    public class ComboT1 : IInitializable, IAsT1, IAsT2
    {
        public ComboT1()
        {
            Debug.Log($"Initialize {nameof(ComboT1)}");
        }

        public void Initialize()
        {
        }

        public void AsT1()
        {
        }

        public void AsT2()
        {
        }
    }
}