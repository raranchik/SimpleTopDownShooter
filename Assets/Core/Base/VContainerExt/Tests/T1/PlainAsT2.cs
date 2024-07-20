using Core.Base.VContainerExt.Attributes;
using UnityEngine;

namespace Core.Base.VContainerExt.Tests.T1
{
    [Register, Context(typeof(ContextT1)), As(typeof(IAsT2)), AsSelf]
    public class PlainAsT2 : IAsT2
    {
        public PlainAsT2()
        {
            Debug.Log($"Initialize {nameof(PlainAsT2)}");
        }

        public void AsT2()
        {
        }
    }
}