using System;

namespace Core.Base.VContainerExt
{
    public interface IVContainerManager
    {
        void InitializeContext(VContainerContextData contextData);
        void DestroyContext(Type contextType);
    }
}