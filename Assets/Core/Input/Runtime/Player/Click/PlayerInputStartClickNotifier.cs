using Core.Base.Notifier;
using Core.Base.VContainerExt.Attributes;
using Core.Infrastructure;
using UnityEngine;

namespace Core.Input.Player.Click
{
    [Register, As(typeof(IPlayerInputStartClickNotifier)), AsSelf, Context(typeof(StartContext))]
    public class PlayerInputStartClickNotifier : IPlayerInputStartClickNotifier
    {
        private readonly Notifier<IPlayerInputStartClickObserver> m_Notifier =
            new Notifier<IPlayerInputStartClickObserver>();

        public void Attach(IPlayerInputStartClickObserver observer)
        {
            m_Notifier.Attach(observer);
        }

        public void Detach(IPlayerInputStartClickObserver observer)
        {
            m_Notifier.Detach(observer);
        }

        public void OnStartClick(Vector2 screenPosition)
        {
            m_Notifier.NotifyAll(x => { x.OnStartClick(screenPosition); });
        }
    }
}