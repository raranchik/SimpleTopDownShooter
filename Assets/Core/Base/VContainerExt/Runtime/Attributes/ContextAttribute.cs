using System;

namespace Core.Base.VContainerExt.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class ContextAttribute : VContainerBaseAttribute
    {
        public Type Type { get; }

        public ContextAttribute(Type type)
        {
            Type = type;
        }
    }
}