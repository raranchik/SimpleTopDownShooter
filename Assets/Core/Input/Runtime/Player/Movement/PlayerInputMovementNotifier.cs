using Core.Base.Notifier;
using Core.Base.VContainerExt.Attributes;
using Core.Infrastructure;
using UnityEngine;

namespace Core.Input.Player.Movement
{
    [Register, As(typeof(IPlayerInputMovementNotifier)), AsSelf, Context(typeof(StartContext))]
    public class PlayerInputMovementNotifier : IPlayerInputMovementNotifier
    {
        private readonly Notifier<IPlayerInputMovementObserver> m_Notifier =
            new Notifier<IPlayerInputMovementObserver>();

        public void Attach(IPlayerInputMovementObserver observer)
        {
            m_Notifier.Attach(observer);
        }

        public void Detach(IPlayerInputMovementObserver observer)
        {
            m_Notifier.Detach(observer);
        }

        public void OnMove(Vector2 direction)
        {
            m_Notifier.NotifyAll(x => x.OnMove(direction));
        }
    }
}