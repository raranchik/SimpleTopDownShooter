using Core.Base.VContainerExt.Attributes;
using Core.Infrastructure.Start;
using Core.Input.Player.Click;
using Core.Input.Player.Fire;
using Core.Input.Player.Movement;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace Core.Input
{
    [RegisterEntryPoint, Context(typeof(StartContext))]
    public class PlayerInputActions : InputActions.IPlayerActions, IInitializable, ITickable
    {
        [Inject] private readonly InputActions m_InputActions;
        [Inject] private readonly MouseService m_MouseService;
        [Inject] private readonly IPlayerInputStartClickNotifier m_StartClick;
        [Inject] private readonly IPlayerInputEndClickNotifier m_EndClick;
        [Inject] private readonly IPlayerInputStartMovementNotifier m_StartMove;
        [Inject] private readonly IPlayerInputMovementNotifier m_Move;
        [Inject] private readonly IPlayerInputEndMovementNotifier m_EndMove;
        [Inject] private readonly IPlayerInputStartFireNotifier m_StartFire;
        [Inject] private readonly IPlayerInputFireNotifier m_Fire;
        [Inject] private readonly IPlayerInputEndFireNotifier m_EndFire;

        private InputActionPhase m_MovePhase;
        private Vector2 m_MoveDirection;
        private InputActionPhase m_FirePhase;
        private Vector2 m_FirePosition;

        public void Initialize()
        {
            m_InputActions.Player.AddCallbacks(this);
        }

        public void Tick()
        {
            if (m_MovePhase == InputActionPhase.Performed)
            {
                m_Move.OnMove(m_MoveDirection);
            }

            if (m_FirePhase == InputActionPhase.Performed)
            {
                m_Fire.OnFire(m_FirePosition);
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            var phase = context.phase;
            if (phase == InputActionPhase.Started)
            {
                m_StartMove.OnStartMove(context.ReadValue<Vector2>().normalized);
                return;
            }

            if (phase == InputActionPhase.Performed)
            {
                m_MoveDirection = context.ReadValue<Vector2>().normalized;
                m_MovePhase = phase;
                return;
            }

            if (phase == InputActionPhase.Canceled)
            {
                m_MovePhase = InputActionPhase.Waiting;
                m_EndMove.OnEndMove();
                return;
            }
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            var phase = context.phase;
            if (phase == InputActionPhase.Started)
            {
                m_StartClick.OnStartClick(m_MouseService.GetPosition());
                return;
            }

            if (phase == InputActionPhase.Canceled)
            {
                m_EndClick.OnEndClick(m_MouseService.GetPosition());
                return;
            }
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            var phase = context.phase;
            if (phase == InputActionPhase.Started)
            {
                m_StartFire.OnStartFire(context.ReadValue<Vector2>());
                return;
            }

            if (phase == InputActionPhase.Performed)
            {
                m_FirePosition = context.ReadValue<Vector2>();
                m_FirePhase = phase;
                return;
            }

            if (phase == InputActionPhase.Canceled)
            {
                m_FirePhase = InputActionPhase.Waiting;
                m_EndFire.OnEndFire();
            }
        }
    }
}