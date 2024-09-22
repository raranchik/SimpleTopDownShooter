using System;
using System.Collections.Generic;
using Core.Base.VContainerExt;
using Core.Infrastructure.Start;
using VContainer;
using VContainer.Unity;

namespace Core.Infrastructure.Application
{
    public class ApplicationLifetimeScope : LifetimeScope
    {
        protected override void Awake()
        {
            base.Awake();
            CreateChild<StartLifetimeScope>();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            var manager = new VContainerManager();
            builder.RegisterInstance(manager);
            var data = new VContainerContextData()
            {
                Builder = builder,
                Context = typeof(ApplicationContext),
                FastSearchNamespacesByClasses = new HashSet<Type>(),
                IsIgnoreNamespaces = true,
                Namespaces = new HashSet<string>(),
                Scope = this,
            };
            manager.InitializeContext(data);
            base.Configure(builder);
        }
    }
}