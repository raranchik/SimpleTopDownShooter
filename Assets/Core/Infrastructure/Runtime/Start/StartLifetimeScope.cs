using System;
using System.Collections.Generic;
using Core.Base.CoroutineRunner;
using Core.Base.Feature;
using Core.Base.VContainerExt;
using VContainer;
using VContainer.Unity;

namespace Core.Infrastructure.Start
{
    public class StartLifetimeScope : LifetimeScope
    {
        [Inject] private readonly IReadOnlyList<IFeature> m_Features;
        [Inject] private readonly ICoroutineRunner m_RoutineRunner;

        protected override void Awake()
        {
            base.Awake();
            Container.Inject(this);
            InitFeatures();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            var manager = Parent.Container.Resolve<VContainerManager>();
            var data = new VContainerContextData()
            {
                Builder = builder,
                Context = typeof(StartContext),
                FastSearchNamespacesByClasses = new HashSet<Type>(),
                IsIgnoreNamespaces = true,
                Namespaces = new HashSet<string>(),
                Scope = this,
            };
            manager.InitializeContext(data);
            base.Configure(builder);
        }

        private void InitFeatures()
        {
            if (m_Features == null)
            {
                return;
            }

            var systems = new SimpleSystems();
            var launcher = new SimpleLauncher();
            var runner = new SimpleRunner(launcher);
            systems.Initialize(m_Features);
            var resourceLoader =
                systems.GetFeature(typeof(AddressablesResourcesLoadingFeature)) as AddressablesResourcesLoadingFeature;
            runner.AddSystem(new SimpleSystem(typeof(ResourcesLoadPhase), new List<Func<bool>>(), () => { }));
            runner.AddSystem(new SimpleSystem(typeof(StartInitPhase), new List<Func<bool>>()
            {
                () => resourceLoader!.IsAllLoaded
            }, () => { }));
            systems.SetByAttributes(runner);
            m_RoutineRunner.StartCoroutine(runner.Initialize());
        }
    }
}