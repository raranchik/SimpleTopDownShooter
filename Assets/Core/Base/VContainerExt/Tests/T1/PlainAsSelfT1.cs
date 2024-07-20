using Core.Base.VContainerExt.Attributes;
using UnityEngine;

namespace Core.Base.VContainerExt.Tests.T1
{
    [Register, Context(typeof(ContextT1)), AsSelf]
    public class PlainAsSelfT1
    {
        public PlainAsSelfT1()
        {
            Debug.Log($"Initialize {nameof(PlainAsSelfT1)}");
        }
    }
}