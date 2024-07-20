using Core.Base.Notifier;
using Core.Base.VContainerExt.Attributes;
using Core.Infrastructure;
using UnityEngine;

namespace Core.Input.Player.Movement
{
    [Register, As(typeof(IPlayerInputStartMovementNotifier)), AsSelf, Context(typeof(StartContext))]
    public class PlayerInputStartMovementNotifier : IPlayerInputStartMovementNotifier
    {
        private readonly Notifier<IPlayerInputStartMovementObserver> m_Notifier =
            new Notifier<IPlayerInputStartMovementObserver>();

        public void Attach(IPlayerInputStartMovementObserver observer)
        {
            m_Notifier.Attach(observer);
        }

        public void Detach(IPlayerInputStartMovementObserver observer)
        {
            m_Notifier.Detach(observer);
        }

        public void OnStartMove(Vector2 direction)
        {
            m_Notifier.NotifyAll(x => x.OnStartMove(direction));
        }
    }
}