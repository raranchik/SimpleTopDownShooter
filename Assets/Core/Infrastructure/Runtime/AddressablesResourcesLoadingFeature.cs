using Core.Base.AddressablesExt;
using Core.Base.CoroutineRunner;
using Core.Base.Feature;
using Core.Base.Feature.Attributes;
using Core.Base.VContainerExt.Attributes;
using Core.Infrastructure.Application;
using VContainer;

namespace Core.Infrastructure
{
    [Register, As(typeof(IFeature)), AsSelf, Context(typeof(ApplicationContext))]
    [System(typeof(ResourcesLoadPhase), 0)]
    public class AddressablesResourcesLoadingFeature : IFeature
    {
        [Inject] private AddressablesLoader m_AddressablesLoader;
        [Inject] private ICoroutineRunner m_RoutineRunner;
        private bool m_IsAllLoaded;

        public bool IsAllLoaded => m_IsAllLoaded;

        public OperationStatus PreInitialize()
        {
            m_IsAllLoaded = false;
            return OperationDefinition.Successful();
        }

        public OperationStatus Initialize()
        {
            m_AddressablesLoader.LoadAll();
            m_RoutineRunner.StartCoroutine(m_AddressablesLoader.CheckAllLoaded(OnLoaded));
            return OperationDefinition.Successful();
        }

        public OperationStatus PostInitialize()
        {
            return OperationDefinition.Successful();
        }

        private void OnLoaded(bool isLoaded)
        {
            m_IsAllLoaded = isLoaded;
        }
    }
}