using System;
using System.Collections.Generic;
using Core.Base.VContainerExt;
using VContainer;
using VContainer.Unity;

namespace Core.Infrastructure
{
    public class RootLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            var data = new VContainerContextData()
            {
                Builder = builder,
                Context = typeof(StartContext),
                FastSearchNamespacesByClasses = new HashSet<Type>(),
                IsIgnoreNamespaces = true,
                Namespaces = new HashSet<string>(),
            };
            var context = new VContainerContext(data);
            context.InitContext();
            base.Configure(builder);
        }
    }
}