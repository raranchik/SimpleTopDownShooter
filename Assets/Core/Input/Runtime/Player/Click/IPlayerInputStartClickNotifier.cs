using Core.Base.Notifier;
using UnityEngine;

namespace Core.Input.Player.Click
{
    public interface IPlayerInputStartClickNotifier : INotifier<IPlayerInputStartClickObserver>
    {
        void OnStartClick(Vector2 screenPosition);
    }
}