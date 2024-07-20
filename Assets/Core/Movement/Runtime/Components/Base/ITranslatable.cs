using UnityEngine;

namespace Core.Movement.Components.Base
{
    public interface ITranslatable
    {
        Vector3 Position { get; }
        void Translate(Vector3 position);
    }
}