using System;
using Core.Base.VContainerExt.Attributes;
using Core.Infrastructure;
using Core.Input.Player.Movement;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Player
{
    [RegisterEntryPoint, AsSelf, Context(typeof(StartContext))]
    public class PlayerMovementSystem : IInitializable, IFixedTickable, IPlayerInputStartMovementObserver,
        IPlayerInputMovementObserver, IPlayerInputEndMovementObserver, IDisposable
    {
        // [Inject] private readonly IPlayerView m_PlayerView;
        [Inject] private readonly PlayerInputStartMovementNotifier m_StartNotifier;
        [Inject] private readonly PlayerInputMovementNotifier m_PerformNotifier;
        [Inject] private readonly PlayerInputEndMovementNotifier m_EndNotifier;

        private Vector2 m_Direction;
        private bool m_IsMoved;

        public void Initialize()
        {
            m_StartNotifier.Attach(this);
            m_PerformNotifier.Attach(this);
            m_EndNotifier.Attach(this);
        }

        public void FixedTick()
        {
            if (!m_IsMoved)
            {
                return;
            }

            Debug.Log(m_Direction.ToString());
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
        }

        public void Dispose()
        {
            m_StartNotifier?.Detach(this);
            m_PerformNotifier?.Detach(this);
            m_EndNotifier?.Detach(this);
        }
    }
}