using System;

namespace Core.Base.VContainerExt.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class AsSelfAttribute : VContainerBaseAttribute
    {
    }
}