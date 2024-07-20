using System;

namespace Core.Base.Feature.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class SystemAttribute : Attribute
    {
        public Type Name { get; }
        public int Order { get; }

        public SystemAttribute(Type name, int order = int.MaxValue)
        {
            Name = name;
            Order = order;
        }
    }
}