using System;
using Core.Base.CameraExt;
using Core.Base.Feature;
using Core.Base.Feature.Attributes;
using Core.Base.Notifier;
using Core.Base.VContainerExt.Attributes;
using Core.Camera;
using Core.Field.Runtime;
using Core.Infrastructure.Start;
using Core.Input.Player.Movement;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Player
{
    [RegisterEntryPoint, AsSelf, Context(typeof(StartContext))]
    [System(typeof(StartInitPhase), order: 100)]
    public class CameraToPlayerFollowTranslateMovementSystem : ILateTickable, IPlayerInputStartMovementObserver,
        IPlayerInputEndMovementObserver, IFeature, IDisposable
    {
        [Inject] private readonly INotifier<IPlayerInputStartMovementObserver> m_StartMoveNotifier;
        [Inject] private readonly INotifier<IPlayerInputEndMovementObserver> m_EndMoveNotifier;
        [Inject] private readonly PlayerFeature m_PlayerFeature;
        [Inject] private readonly MainCameraMarker m_MainCameraMarker;
        [Inject] private readonly FieldService m_FieldService;
        [Inject] private readonly CameraHelper m_CameraHelper;

        private Transform m_MainCameraTransform;
        private UnityEngine.Camera m_MainCamera;
        private PlayerView m_PlayerView;
        private bool m_IsMoved;
        private (Vector2, Vector2) m_MovementBounds;

        public OperationStatus PreInitialize()
        {
            return OperationDefinition.Successful();
        }

        public OperationStatus Initialize()
        {
            m_PlayerView = m_PlayerFeature.GetPlayerView();
            m_MainCameraTransform = m_MainCameraMarker.Transform;
            m_MainCamera = m_MainCameraMarker.Camera;
            m_MovementBounds = CalculateMovementBounds();
            return OperationDefinition.Successful();
        }

        public OperationStatus PostInitialize()
        {
            m_StartMoveNotifier.Attach(this);
            m_EndMoveNotifier.Attach(this);
            return OperationDefinition.Successful();
        }

        public void LateTick()
        {
            if (!m_IsMoved)
            {
                return;
            }

            var playerPos = m_PlayerView.Position;
            var pos = ClampPosByMovementBounds(playerPos);
            pos.z = m_MainCameraMarker.DefaultAxisZ;
            m_MainCameraTransform.position = pos;
        }

        public void OnStartMove(Vector2 direction)
        {
            m_IsMoved = true;
        }

        public void OnEndMove()
        {
            m_IsMoved = false;
        }

        public void Dispose()
        {
            m_StartMoveNotifier?.Detach(this);
            m_EndMoveNotifier?.Detach(this);
        }

        private Vector3 ClampPosByMovementBounds(Vector3 pos)
        {
            pos.x = Mathf.Clamp(pos.x, m_MovementBounds.Item1.x, m_MovementBounds.Item1.y);
            pos.y = Mathf.Clamp(pos.y, m_MovementBounds.Item2.x, m_MovementBounds.Item2.y);
            return pos;
        }

        private (Vector2, Vector2) CalculateMovementBounds()
        {
            var height = m_MainCamera.orthographicSize;
            var width = height * m_MainCamera.aspect;
            var fieldSize = m_FieldService.GetFieldSize();
            var halfFieldSize = fieldSize * 0.5f;
            var xBounds = new Vector2()
            {
                x = width - halfFieldSize.x,
                y = halfFieldSize.x - width,
            };
            var yBounds = new Vector2()
            {
                x = height - halfFieldSize.y,
                y = halfFieldSize.y - height,
            };

            return (xBounds, yBounds);
        }
    }
}