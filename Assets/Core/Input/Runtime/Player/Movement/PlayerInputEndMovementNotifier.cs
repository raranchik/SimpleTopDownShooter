using Core.Base.Notifier;
using Core.Base.VContainerExt.Attributes;
using Core.Infrastructure;

namespace Core.Input.Player.Movement
{
    [Register, As(typeof(IPlayerInputEndMovementNotifier)), AsSelf, Context(typeof(StartContext))]
    public class PlayerInputEndMovementNotifier : IPlayerInputEndMovementNotifier
    {
        private readonly Notifier<IPlayerInputEndMovementObserver> m_Notifier =
            new Notifier<IPlayerInputEndMovementObserver>();

        public void Attach(IPlayerInputEndMovementObserver observer)
        {
            m_Notifier.Attach(observer);
        }

        public void Detach(IPlayerInputEndMovementObserver observer)
        {
            m_Notifier.Detach(observer);
        }

        public void OnEndMove()
        {
            m_Notifier.NotifyAll(x => x.OnEndMove());
        }
    }
}