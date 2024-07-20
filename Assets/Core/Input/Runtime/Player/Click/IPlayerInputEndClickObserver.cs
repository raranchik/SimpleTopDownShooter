using UnityEngine;

namespace Core.Input.Player.Click
{
    public interface IPlayerInputEndClickObserver
    {
        public void OnEndClick(Vector2 screenPosition);
    }
}