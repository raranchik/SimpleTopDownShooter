using Core.Base.AddressablesExt;
using Core.Base.CameraExt;
using Core.Base.Configs;
using Core.Base.CoroutineRunner;
using Core.Base.VContainerExt.Attributes;

namespace Core.Infrastructure.Application
{
    [Configurator, Context(typeof(ApplicationContext))]
    public class ApplicationConfigurator
    {
        private readonly ISerializer m_Serializer = new NewtonsoftSerializer();
        private readonly AddressablesLoader m_AddressablesLoader = new AddressablesLoader();
        private readonly ICoroutineRunner m_RoutineRunner = new PlainCoroutineRunner();
        private readonly CameraHelper m_CameraHelper = new CameraHelper();

        [Bean]
        public ISerializer GetSerializer()
        {
            return m_Serializer;
        }

        [Bean]
        public AddressablesLoader GetAddressablesLoader()
        {
            return m_AddressablesLoader;
        }

        [Bean]
        public ICoroutineRunner GetRoutineRunner()
        {
            return m_RoutineRunner;
        }

        [Bean]
        public CameraHelper GetCameraHelper()
        {
            return m_CameraHelper;
        }
    }
}