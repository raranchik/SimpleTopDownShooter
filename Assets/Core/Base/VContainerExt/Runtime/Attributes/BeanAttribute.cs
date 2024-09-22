using System;

namespace Core.Base.VContainerExt.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class BeanAttribute : VContainerBaseAttribute
    {
    }
}