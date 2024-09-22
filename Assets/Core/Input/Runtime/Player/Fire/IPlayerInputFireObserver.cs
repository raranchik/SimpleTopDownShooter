using UnityEngine;

namespace Core.Input.Player.Fire
{
    public interface IPlayerInputFireObserver
    {
        public void OnFire(Vector2 screenPosition);
    }
}