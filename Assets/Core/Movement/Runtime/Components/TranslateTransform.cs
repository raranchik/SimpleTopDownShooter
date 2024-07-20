using Core.Movement.Components.Base;
using UnityEngine;

namespace Core.Movement.Components
{
    public class TranslateTransform : MonoBehaviour, ITranslatable
    {
        [SerializeField] private Transform m_Transform;

        public Vector3 Position => m_Transform.position;

        public void Translate(Vector3 position)
        {
            m_Transform.Translate(position);
        }
    }
}