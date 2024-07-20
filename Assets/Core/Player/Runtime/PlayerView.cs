using Core.Movement.Components;
using Core.Movement.Components.Base;
using UnityEngine;

namespace Core.Player
{
    public class PlayerView : MonoBehaviour, ITranslatable, IRotatable
    {
        [SerializeField] private TranslateRigidbody2D m_Translate;
        [SerializeField] private RotateRigidbody2D m_Rotate;

        public Vector3 Position => m_Translate.Position;
        public Quaternion Rotation => m_Rotate.Rotation;

        public void Translate(Vector3 position)
        {
            m_Translate.Translate(position);
        }

        public void Rotate(Quaternion rotation)
        {
            m_Rotate.Rotate(rotation);
        }
    }
}