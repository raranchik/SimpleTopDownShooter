using Core.Base.VContainerExt.Attributes;
using UnityEngine;
using VContainer.Unity;

namespace Core.Base.VContainerExt.Tests.T1
{
    [Register, Context(typeof(ContextT1)), As(typeof(IInitializable), typeof(IAsT1), typeof(IAsT2)), AsSelf]
    public class ComboT2 : IInitializable, IAsT1, IAsT2
    {
        public ComboT2()
        {
            Debug.Log($"Initialize {nameof(ComboT2)}");
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