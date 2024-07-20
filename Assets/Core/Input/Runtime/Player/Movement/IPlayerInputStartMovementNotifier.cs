using Core.Base.Notifier;
using UnityEngine;

namespace Core.Input.Player.Movement
{
    public interface IPlayerInputStartMovementNotifier : INotifier<IPlayerInputStartMovementObserver>
    {
        void OnStartMove(Vector2 direction);
    }
}