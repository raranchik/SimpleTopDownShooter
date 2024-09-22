using System;
using Core.Base.CameraExt;
using Core.Base.Feature;
using Core.Base.Feature.Attributes;
using Core.Base.Notifier;
using Core.Base.VContainerExt.Attributes;
using Core.Camera;
using Core.Infrastructure.Start;
using Core.Input.Player.Fire;
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
    public class PlayerMovementRotationSystem : IFixedTickable, IPlayerInputStartMovementObserver,
        IPlayerInputMovementObserver, IPlayerInputEndMovementObserver, IPlayerInputStartFireObserver,
        IPlayerInputFireObserver, IPlayerInputEndFireObserver, IFeature, IDisposable
    {
        [Inject] private readonly INotifier<IPlayerInputStartMovementObserver> m_StartMoveNotifier;
        [Inject] private readonly INotifier<IPlayerInputMovementObserver> m_MoveNotifier;
        [Inject] private readonly INotifier<IPlayerInputEndMovementObserver> m_EndMoveNotifier;
        [Inject] private readonly INotifier<IPlayerInputStartFireObserver> m_StartFireNotifier;
        [Inject] private readonly INotifier<IPlayerInputFireObserver> m_FireNotifier;
        [Inject] private readonly INotifier<IPlayerInputEndFireObserver> m_EndFireNotifier;
        [Inject] private readonly PlayerFeature m_PlayerFeature;
        [Inject] private readonly MovementHelper m_MovementHelper;
        [Inject] private readonly CameraHelper m_CameraHelper;
        [Inject] private readonly MainCameraMarker m_MainCameraMarker;

        private UnityEngine.Camera m_MainCamera;
        private PlayerView m_PlayerView;
        private PlayerMovementConfig m_PlayerMovement;
        private Vector2 m_Direction;
        private bool m_IsMoving;
        private bool m_IsFiring;

        public OperationStatus PreInitialize()
        {
            return OperationDefinition.Successful();
        }

        public OperationStatus Initialize()
        {
            m_PlayerView = m_PlayerFeature.GetPlayerView();
            var playerConfig = m_PlayerFeature.GetPlayerConfig();
            m_PlayerMovement = playerConfig.Movement;
            m_MainCamera = m_MainCameraMarker.Camera;
            return OperationDefinition.Successful();
        }

        public OperationStatus PostInitialize()
        {
            m_StartMoveNotifier.Attach(this);
            m_MoveNotifier.Attach(this);
            m_EndMoveNotifier.Attach(this);
            m_StartFireNotifier.Attach(this);
            m_FireNotifier.Attach(this);
            m_EndFireNotifier.Attach(this);
            return OperationDefinition.Successful();
        }

        public void FixedTick()
        {
            if (!m_IsMoving && !m_IsFiring)
            {
                return;
            }

            var rot = m_PlayerView.Rotation;
            var nextRot =
                m_MovementHelper.CalcSmoothRot(rot, m_Direction, m_PlayerMovement.RotationSpeed, Time.deltaTime);
            m_PlayerView.Rotate(nextRot);
        }

        public void OnStartMove(Vector2 direction)
        {
            m_IsMoving = true;
            if (m_IsFiring)
            {
                return;
            }

            m_Direction = direction;
        }

        public void OnMove(Vector2 direction)
        {
            if (m_IsFiring)
            {
                return;
            }

            m_Direction = direction;
        }

        public void OnEndMove()
        {
            m_IsMoving = false;
        }

        public void OnStartFire(Vector2 screenPosition)
        {
            var firePos = m_CameraHelper.ScreenToWorldPoint(m_MainCamera, screenPosition);
            var playerPos = m_PlayerView.Position;
            m_Direction = (firePos - playerPos).normalized;
            m_IsFiring = true;
        }

        public void OnFire(Vector2 screenPosition)
        {
            var firePos = m_CameraHelper.ScreenToWorldPoint(m_MainCamera, screenPosition);
            var playerPos = m_PlayerView.Position;
            m_Direction = (firePos - playerPos).normalized;
        }

        public void OnEndFire()
        {
            m_IsFiring = false;
        }

        public void Dispose()
        {
            m_StartMoveNotifier.Detach(this);
            m_MoveNotifier.Detach(this);
            m_EndMoveNotifier.Detach(this);
            m_StartFireNotifier.Detach(this);
            m_FireNotifier.Detach(this);
            m_EndFireNotifier.Detach(this);
        }
    }
}