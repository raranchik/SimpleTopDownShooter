using Core.Base.VContainerExt.Attributes;
using Core.Infrastructure.Start;
using UnityEngine;
using VContainer;

namespace Core.Field.Runtime
{
    [Register, AsSelf, Context(typeof(StartContext))]
    public class FieldService : IFieldService
    {
        [Inject] private readonly FieldView m_FieldView;

        public Vector3 GetFieldSize()
        {
            return m_FieldView.Size;
        }
    }
}