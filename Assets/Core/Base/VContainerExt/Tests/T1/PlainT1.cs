using Core.Base.VContainerExt.Attributes;
using UnityEngine;

namespace Core.Base.VContainerExt.Tests.T1
{
    [Register, Context(typeof(ContextT1)), AsSelf]
    public class PlainT1
    {
        public PlainT1()
        {
            Debug.Log($"Initialize {nameof(PlainT1)}");
        }
    }
}