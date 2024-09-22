using Core.Base.VContainerExt.Attributes;
using Core.Infrastructure.Application;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Input
{
    [Register, Context(typeof(ApplicationContext))]
    public class MouseService
    {
        private readonly Mouse m_Current;

        public MouseService()
        {
            m_Current = Mouse.current;
        }

        public Vector2 GetPosition()
        {
            return m_Current.position.ReadValue();
        }
    }
}