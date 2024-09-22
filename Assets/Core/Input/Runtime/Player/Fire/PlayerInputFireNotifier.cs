using Core.Base.Notifier;
using Core.Base.VContainerExt.Attributes;
using Core.Infrastructure.Start;
using UnityEngine;

namespace Core.Input.Player.Fire
{
    [Register, As(typeof(IPlayerInputFireNotifier), typeof(INotifier<IPlayerInputFireObserver>)),
     AsSelf, Context(typeof(StartContext))]
    public class PlayerInputFireNotifier : IPlayerInputFireNotifier
    {
        private readonly Notifier<IPlayerInputFireObserver> m_Notifier =
            new Notifier<IPlayerInputFireObserver>();

        public void Attach(IPlayerInputFireObserver observer)
        {
            m_Notifier.Attach(observer);
        }

        public void Detach(IPlayerInputFireObserver observer)
        {
            m_Notifier.Detach(observer);
        }

        public void OnFire(Vector2 screenPosition)
        {
            m_Notifier.NotifyAll(x => { x.OnFire(screenPosition); });
        }
    }
}