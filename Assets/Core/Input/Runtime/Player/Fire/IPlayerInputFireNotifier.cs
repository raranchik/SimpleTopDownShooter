using Core.Base.Notifier;
using UnityEngine;

namespace Core.Input.Player.Fire
{
    public interface IPlayerInputFireNotifier : INotifier<IPlayerInputFireObserver>
    {
        void OnFire(Vector2 screenPosition);
    }
}