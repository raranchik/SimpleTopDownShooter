using Core.Base.VContainerExt.Attributes;
using UnityEngine;

namespace Core.Base.VContainerExt.Tests.T1
{
    [Register, Context(typeof(ContextT1)), As(typeof(IAsT1)), AsSelf]
    public class PlainAsT1 : IAsT1
    {
        public PlainAsT1()
        {
            Debug.Log($"Initialize {nameof(PlainAsT1)}");
        }

        public void AsT1()
        {
        }
    }
}