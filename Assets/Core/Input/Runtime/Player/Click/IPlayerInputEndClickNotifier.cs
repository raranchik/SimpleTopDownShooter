using Core.Base.Notifier;
using UnityEngine;

namespace Core.Input.Player.Click
{
    public interface IPlayerInputEndClickNotifier : INotifier<IPlayerInputEndClickObserver>
    {
        void OnEndClick(Vector2 screenPosition);
    }
}