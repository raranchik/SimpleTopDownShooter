using UnityEngine;

namespace Core.Zones.Death
{
    public class DeathZoneView : MonoBehaviour, IDeathViewZone
    {
        [SerializeField] private Transform m_Transform;

        public Vector3 Position => m_Transform.position;

        public void SetPosition(Vector3 position)
        {
            m_Transform.position = position;
        }

        public void SetScale(Vector3 scale)
        {
            m_Transform.localScale = scale;
        }
    }
}