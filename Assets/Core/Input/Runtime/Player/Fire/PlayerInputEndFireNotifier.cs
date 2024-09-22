using Core.Base.Notifier;
using Core.Base.VContainerExt.Attributes;
using Core.Infrastructure.Start;

namespace Core.Input.Player.Fire
{
    [Register, As(typeof(IPlayerInputEndFireNotifier), typeof(INotifier<IPlayerInputEndFireObserver>)),
     AsSelf, Context(typeof(StartContext))]
    public class PlayerInputEndFireNotifier : IPlayerInputEndFireNotifier
    {
        private readonly Notifier<IPlayerInputEndFireObserver> m_Notifier =
            new Notifier<IPlayerInputEndFireObserver>();

        public void Attach(IPlayerInputEndFireObserver observer)
        {
            m_Notifier.Attach(observer);
        }

        public void Detach(IPlayerInputEndFireObserver observer)
        {
            m_Notifier.Detach(observer);
        }

        public void OnEndFire()
        {
            m_Notifier.NotifyAll(x => { x.OnEndFire(); });
        }
    }
}