using UnityEngine;

namespace Core.Input.Player.Fire
{
    public interface IPlayerInputStartFireObserver
    {
        public void OnStartFire(Vector2 screenPosition);
    }
}