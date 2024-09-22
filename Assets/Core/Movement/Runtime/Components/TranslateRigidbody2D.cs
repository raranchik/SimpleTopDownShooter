using Core.Movement.Components.Base;
using UnityEngine;

namespace Core.Movement.Components
{
    public class TranslateRigidbody2D : MonoBehaviour, ITranslatable
    {
        [SerializeField] private Rigidbody2D m_Rigidbody2D;

        public Vector3 Position => m_Rigidbody2D.position;

        public void Translate(Vector3 position)
        {
            m_Rigidbody2D.velocity = position;
        }
    }
}