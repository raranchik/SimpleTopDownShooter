using Core.Base.VContainerExt.Attributes;
using UnityEngine;
using VContainer.Unity;

namespace Core.Base.VContainerExt.Tests.T1
{
    [Register, Context(typeof(ContextT1)), AsImplementedInterfaces, AsSelf]
    public class ComboT3 : IInitializable, IAsT1, IAsT2
    {
        public ComboT3()
        {
            Debug.Log($"Initialize {nameof(ComboT3)}");
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