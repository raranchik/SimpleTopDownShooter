using UnityEngine;

namespace Core.Input.Player.Movement
{
    public interface IPlayerInputStartMovementObserver
    {
        void OnStartMove(Vector2 direction);
    }
}