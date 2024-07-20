using Core.Base.Notifier;
using UnityEngine;

namespace Core.Input.Player.Movement
{
    public interface IPlayerInputMovementNotifier : INotifier<IPlayerInputMovementObserver>
    {
        void OnMove(Vector2 direction);
    }
}