using System;
using System.Collections.Generic;
using VContainer;

namespace Core.Base.VContainerExt
{
    public class VContainerContextData
    {
        public IContainerBuilder Builder;
        public ISet<string> Namespaces;
        public ISet<Type> FastSearchNamespacesByClasses;
        public bool IsIgnoreNamespaces;
        public Type Context;
    }
}