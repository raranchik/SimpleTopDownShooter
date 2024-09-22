using System;
using Core.Base.Feature;
using Core.Base.Feature.Attributes;
using Core.Base.Notifier;
using Core.Base.VContainerExt.Attributes;
using Core.Infrastructure.Start;
using Core.Input.Player.Movement;
using Core.Movement.Base;
using Core.Player.Configs;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Player
{
    [RegisterEntryPoint, AsSelf, Context(typeof(StartContext))]
    [System(typeof(StartInitPhase), order: 75)]
    public class PlayerMovementTranslateSystem : IFixedTickable, IPlayerInputStartMovementObserver,
        IPlayerInputMovementObserver, IPlayerInputEndMovementObserver, IFeature, IDisposable
    {
        [Inject] private readonly INotifier<IPlayerInputStartMovementObserver> m_StartMoveNotifier;
        [Inject] private readonly INotifier<IPlayerInputMovementObserver> m_MoveNotifier;
        [Inject] private readonly INotifier<IPlayerInputEndMovementObserver> m_EndMoveNotifier;
        [Inject] private PlayerFeature m_PlayerFeature;
        [Inject] private MovementHelper m_MovementHelper;

        private PlayerView m_PlayerView;
        private PlayerMovementConfig m_PlayerMovement;
        private Vector2 m_Direction;
        private bool m_IsMoved;

        public OperationStatus PreInitialize()
        {
            return OperationDefinition.Successful();
        }

        public OperationStatus Initialize()
        {
            m_PlayerView = m_PlayerFeature.GetPlayerView();
            var playerConfig = m_PlayerFeature.GetPlayerConfig();
            m_PlayerMovement = playerConfig.Movement;
            return OperationDefinition.Successful();
        }

        public OperationStatus PostInitialize()
        {
            m_StartMoveNotifier.Attach(this);
            m_MoveNotifier.Attach(this);
            m_EndMoveNotifier.Attach(this);
            return OperationDefinition.Successful();
        }

        public void FixedTick()
        {
            if (!m_IsMoved)
            {
                return;
            }

            m_PlayerView.Translate(m_PlayerMovement.MoveSpeed * m_Direction);
        }

        public void OnStartMove(Vector2 direction)
        {
            m_Direction = direction;
            m_IsMoved = true;
        }

        public void OnMove(Vector2 direction)
        {
            m_Direction = direction;
        }

        public void OnEndMove()
        {
            m_IsMoved = false;
            m_PlayerView.Translate(Vector3.zero);
        }

        public void Dispose()
        {
            m_StartMoveNotifier?.Detach(this);
            m_MoveNotifier?.Detach(this);
            m_EndMoveNotifier?.Detach(this);
        }
    }
}