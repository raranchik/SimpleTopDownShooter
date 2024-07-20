using Core.Base.VContainerExt.Attributes;
using UnityEngine;

namespace Core.Base.VContainerExt.Tests.T1
{
    [Register, Context(typeof(ContextT1)), AsImplementedInterfaces, AsSelf]
    public class PlainAsImplementedInterfacesT1 : IAsT1
    {
        public PlainAsImplementedInterfacesT1()
        {
            Debug.Log($"Initialize {nameof(PlainAsImplementedInterfacesT1)}");
        }

        public void AsT1()
        {
        }
    }
}