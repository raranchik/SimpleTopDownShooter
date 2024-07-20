using Core.Base.VContainerExt.Attributes;
using Core.Infrastructure;
using Core.Input.Player.Click;
using Core.Input.Player.Movement;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace Core.Input
{
    [RegisterEntryPoint, Context(typeof(StartContext))]
    public class PlayerInputActions : InputActions.IPlayerActions, IInitializable
    {
        [Inject] private readonly InputActions m_InputActions;
        [Inject] private readonly MouseService m_MouseService;
        [Inject] private readonly IPlayerInputStartClickNotifier m_StartClick;
        [Inject] private readonly IPlayerInputEndClickNotifier m_EndClick;
        [Inject] private readonly IPlayerInputStartMovementNotifier m_StartMovement;
        [Inject] private readonly IPlayerInputMovementNotifier m_Movement;
        [Inject] private readonly IPlayerInputEndMovementNotifier m_EndMovement;

        public void Initialize()
        {
            m_InputActions.Player.AddCallbacks(this);
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            var phase = context.phase;
            if (phase == InputActionPhase.Started)
            {
                m_StartMovement.OnStartMove(context.ReadValue<Vector2>());
                return;
            }

            if (phase == InputActionPhase.Performed)
            {
                m_Movement.OnMove(context.ReadValue<Vector2>());
                return;
            }

            if (phase == InputActionPhase.Canceled)
            {
                m_EndMovement.OnEndMove();
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
                return;
            }

            if (phase == InputActionPhase.Performed)
            {
                return;
            }
        }
    }
}