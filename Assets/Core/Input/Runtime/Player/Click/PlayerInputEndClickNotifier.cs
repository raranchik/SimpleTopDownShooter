using Core.Base.Notifier;
using Core.Base.VContainerExt.Attributes;
using Core.Infrastructure.Start;
using UnityEngine;

namespace Core.Input.Player.Click
{
    [Register, As(typeof(IPlayerInputEndClickNotifier), typeof(INotifier<IPlayerInputEndClickObserver>)),
     AsSelf, Context(typeof(StartContext))]
    public class PlayerInputEndClickNotifier : IPlayerInputEndClickNotifier
    {
        private readonly Notifier<IPlayerInputEndClickObserver> m_Notifier =
            new Notifier<IPlayerInputEndClickObserver>();

        public void Attach(IPlayerInputEndClickObserver observer)
        {
            m_Notifier.Attach(observer);
        }

        public void Detach(IPlayerInputEndClickObserver observer)
        {
            m_Notifier.Detach(observer);
        }

        public void OnEndClick(Vector2 screenPosition)
        {
            m_Notifier.NotifyAll(x => { x.OnEndClick(screenPosition); });
        }
    }
}