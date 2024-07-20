using Core.Base.VContainerExt.Attributes;
using UnityEngine;

namespace Core.Base.VContainerExt.Tests.T1
{
    [Register, Context(typeof(ContextT1)), AsSelf]
    public class PlainT2
    {
        public PlainT2()
        {
            Debug.Log($"Initialize {nameof(PlainT2)}");
        }
    }
}