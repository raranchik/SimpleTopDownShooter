using Core.Base.VContainerExt.Attributes;
using UnityEngine;

namespace Core.Base.VContainerExt.Tests.T1
{
    [Register, Context(typeof(ContextT1)), AsImplementedInterfaces, AsSelf]
    public class PlainAsImplementedInterfacesT3 : IAsT1, IAsT2
    {
        public PlainAsImplementedInterfacesT3()
        {
            Debug.Log($"Initialize {nameof(PlainAsImplementedInterfacesT3)}");
        }

        public void AsT1()
        {
        }

        public void AsT2()
        {
        }
    }
}