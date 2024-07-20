using UnityEngine;

namespace Core.Input.Player.Movement
{
    public interface IPlayerInputMovementObserver
    {
        void OnMove(Vector2 direction);
    }
}