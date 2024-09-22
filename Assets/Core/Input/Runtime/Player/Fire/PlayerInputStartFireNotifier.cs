using Core.Base.Notifier;
using Core.Base.VContainerExt.Attributes;
using Core.Infrastructure.Start;
using UnityEngine;

namespace Core.Input.Player.Fire
{
    [Register, As(typeof(IPlayerInputStartFireNotifier), typeof(INotifier<IPlayerInputStartFireObserver>)),
     AsSelf, Context(typeof(StartContext))]
    public class PlayerInputStartFireNotifier : IPlayerInputStartFireNotifier
    {
        private readonly Notifier<IPlayerInputStartFireObserver> m_Notifier =
            new Notifier<IPlayerInputStartFireObserver>();

        public void Attach(IPlayerInputStartFireObserver observer)
        {
            m_Notifier.Attach(observer);
        }

        public void Detach(IPlayerInputStartFireObserver observer)
        {
            m_Notifier.Detach(observer);
        }

        public void OnStartFire(Vector2 screenPosition)
        {
            m_Notifier.NotifyAll(x => { x.OnStartFire(screenPosition); });
        }
    }
}