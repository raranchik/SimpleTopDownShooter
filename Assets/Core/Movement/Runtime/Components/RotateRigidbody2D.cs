using Core.Movement.Base;
using Core.Movement.Components.Base;
using UnityEngine;

namespace Core.Movement.Components
{
    public class RotateRigidbody2D : MonoBehaviour, IRotatable
    {
        [SerializeField] private Rigidbody2D m_Rigidbody2D;

        public Quaternion Rotation => Quaternion.AngleAxis(m_Rigidbody2D.rotation, MovementDefinition.AxisUp2D);

        public void Rotate(Quaternion rotation)
        {
            m_Rigidbody2D.SetRotation(rotation);
        }
    }
}