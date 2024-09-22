using System;
using System.Collections.Generic;
using Core.Base.VContainerExt;
using VContainer;
using VContainer.Unity;

namespace Core.Infrastructure.Core
{
    public class CoreLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            var data = new VContainerContextData()
            {
                Builder = builder,
                Context = typeof(CoreContext),
                FastSearchNamespacesByClasses = new HashSet<Type>(),
                IsIgnoreNamespaces = true,
                Namespaces = new HashSet<string>(),
            };
            var context = new VContainerContext(data);
            context.Initialize();
            base.Configure(builder);
        }
    }
}