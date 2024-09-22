using System;
using System.Collections.Generic;
using VContainer;
using VContainer.Unity;

namespace Core.Base.VContainerExt.Tests.T1
{
    public class TestLifetimeScopeContextT1 : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            var contextData = new VContainerContextData()
            {
                Builder = builder,
                Context = typeof(ContextT1),
                FastSearchNamespacesByClasses = new HashSet<Type>() { GetType() },
                IsIgnoreNamespaces = true,
                Namespaces = new HashSet<string>(),
            };
            var context = new VContainerContext(contextData);
            context.Initialize();
            base.Configure(builder);
        }
    }
}