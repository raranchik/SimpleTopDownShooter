using Core.Base.Notifier;
using UnityEngine;

namespace Core.Input.Player.Fire
{
    public interface IPlayerInputStartFireNotifier : INotifier<IPlayerInputStartFireObserver>
    {
        void OnStartFire(Vector2 screenPosition);
    }
}