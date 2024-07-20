using Core.Base.VContainerExt.Attributes;
using UnityEngine;

namespace Core.Base.VContainerExt.Tests.T1
{
    [Register, Context(typeof(ContextT1)), AsImplementedInterfaces, AsSelf]
    public class PlainAsImplementedInterfacesT2 : IAsT2
    {
        public PlainAsImplementedInterfacesT2()
        {
            Debug.Log($"Initialize {nameof(PlainAsImplementedInterfacesT2)}");
        }

        public void AsT2()
        {
        }
    }
}