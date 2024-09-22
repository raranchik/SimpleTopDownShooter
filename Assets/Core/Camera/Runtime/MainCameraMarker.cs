using Core.Base.VContainerExt.Attributes;
using Core.Infrastructure.Start;
using UnityEngine;
using VContainer.Unity;

namespace Core.Camera
{
    [MonoInHierarchy, As(typeof(IInitializable)), AsSelf, Context(typeof(StartContext))]
    public class MainCameraMarker : MonoBehaviour, IInitializable
    {
        [SerializeField] private UnityEngine.Camera m_Camera;
        [SerializeField] private Transform m_Transform;

        private float m_DefaultAxisZ;

        public UnityEngine.Camera Camera => m_Camera;
        public Transform Transform => m_Transform;
        public float DefaultAxisZ => m_DefaultAxisZ;

        public void Initialize()
        {
            m_DefaultAxisZ = m_Transform.position.z;
        }
    }
}