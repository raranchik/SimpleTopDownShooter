using Core.Movement.Components.Base;
using UnityEngine;

namespace Core.Movement.Components
{
    public class RotateTransform : MonoBehaviour, IRotatable
    {
        [SerializeField] private Transform m_Transform;

        public Quaternion Rotation => m_Transform.rotation;
        
        public void Rotate(Quaternion rotation)
        {
            m_Transform.rotation = rotation;
        }
    }
}