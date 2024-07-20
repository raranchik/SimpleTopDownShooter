using System;
using VContainer;

namespace Core.Base.VContainerExt.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class LifetimeAttribute : VContainerBaseAttribute
    {
        public Lifetime Scope { get; }

        public LifetimeAttribute(Lifetime scope)
        {
            Scope = scope;
        }
    }
}