using Core.Base.VContainerExt.Attributes;
using UnityEngine;
using VContainer.Unity;

namespace Core.Base.VContainerExt.Tests.T1
{
    [Register, Context(typeof(ContextT1)), AsSelf]
    public class ComboT4 : IInitializable, IAsT1, IAsT2
    {
        public ComboT4()
        {
            Debug.Log($"Initialize {nameof(ComboT4)}");
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