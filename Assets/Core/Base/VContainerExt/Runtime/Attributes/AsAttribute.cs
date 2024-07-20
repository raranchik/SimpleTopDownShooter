using System;

namespace Core.Base.VContainerExt.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class AsAttribute : VContainerBaseAttribute
    {
        public Type[] Types { get; }

        public AsAttribute(Type type)
        {
            Types = new Type[] { type };
        }

        public AsAttribute(params Type[] types)
        {
            Types = types;
        }
    }
}