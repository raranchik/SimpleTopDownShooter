using Core.Base.VContainerExt.Attributes;
using UnityEngine;

namespace Core.Base.VContainerExt.Tests.T1
{
    [Register, Context(typeof(ContextT1)), As(typeof(IAsT1)), As(typeof(IAsT2)), AsSelf]
    public class PlainAsT3 : IAsT1, IAsT2
    {
        public PlainAsT3()
        {
            Debug.Log($"Initialize {nameof(PlainAsT3)}");
        }

        public void AsT1()
        {
        }

        public void AsT2()
        {
        }
    }
}