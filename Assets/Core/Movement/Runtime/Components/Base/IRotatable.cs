using UnityEngine;

namespace Core.Movement.Components.Base
{
    public interface IRotatable
    {
        Quaternion Rotation { get; }
        void Rotate(Quaternion rotation);
    }
}