using UnityEngine;

namespace Core.Input.Player.Click
{
    public interface IPlayerInputStartClickObserver
    {
        public void OnStartClick(Vector2 screenPosition);
    }
}