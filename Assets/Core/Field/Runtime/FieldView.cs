using Core.Base.VContainerExt.Attributes;
using Core.Infrastructure.Start;
using UnityEngine;
using VContainer.Unity;

namespace Core.Field.Runtime
{
    [MonoInHierarchy, As(typeof(IInitializable)), AsSelf, Context(typeof(StartContext))]
    public class FieldView : MonoBehaviour, IInitializable
    {
        [SerializeField] private Transform m_Transform;

        private Vector3 m_Size;

        public Vector3 Size => m_Size;

        public void Initialize()
        {
            m_Size = m_Transform.lossyScale;
        }
    }
}