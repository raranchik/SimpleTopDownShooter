using Core.Base.VContainerExt.Attributes;
using Core.Infrastructure;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Input
{
    [Register, Context(typeof(StartContext))]
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