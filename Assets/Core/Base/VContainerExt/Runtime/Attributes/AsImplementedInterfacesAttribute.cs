using System;

namespace Core.Base.VContainerExt.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class AsImplementedInterfacesAttribute : VContainerBaseAttribute
    {
    }
}