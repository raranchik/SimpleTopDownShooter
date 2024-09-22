using System;
using Core.Base.AddressablesExt;
using Core.Base.Configs;
using Core.Base.Feature;
using Core.Base.Feature.Attributes;
using Core.Base.VContainerExt.Attributes;
using Core.Infrastructure.Start;
using Core.Player.Configs;
using UnityEngine;
using VContainer;
using Object = UnityEngine.Object;

namespace Core.Player
{
    [Register, As(typeof(IFeature)), AsSelf, Context(typeof(StartContext))]
    [System(typeof(StartInitPhase), order: 50)]
    public class PlayerFeature : IFeature, IDisposable
    {
        [Inject] private readonly ISerializer m_Serializer;
        [Inject] private readonly AddressablesLoader m_AddressablesLoader;
        private AddressableInfo<GameObject> m_PlayerViewInfo;
        private AddressableInfo<TextAsset> m_PlayerConfigInfo;
        private PlayerConfig m_PlayerConfig;
        private PlayerView m_PlayerView;

        public OperationStatus PreInitialize()
        {
            m_PlayerViewInfo = m_AddressablesLoader.GetOrLoadAddressableAsset<GameObject>(
                PlayerDefinition.PlayerViewAddress);
            m_PlayerConfigInfo = m_AddressablesLoader.GetOrLoadAddressableAsset<TextAsset>(
                PlayerDefinition.PlayerConfigAddress);
            return OperationDefinition.Successful();
        }

        public OperationStatus Initialize()
        {
            m_PlayerConfig = m_Serializer.Deserialize<PlayerConfig>(m_PlayerConfigInfo.Result.text);
            var playerViewInstance = Object.Instantiate(m_PlayerViewInfo.Result);
            m_PlayerView = playerViewInstance.GetComponent<PlayerView>();
            return OperationDefinition.Successful();
        }

        public OperationStatus PostInitialize()
        {
            return OperationDefinition.Successful();
        }

        public PlayerView GetPlayerView()
        {
            return m_PlayerView;
        }

        public PlayerConfig GetPlayerConfig()
        {
            return m_PlayerConfig;
        }

        public void Dispose()
        {
        }
    }
}