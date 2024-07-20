using System;
using System.Collections;

namespace Core.Base.Feature
{
    public interface IRunner
    {
        IRunner AddSystem(ISystem system);
        IEnumerator InitializeSystems();
        ISystem GetSystem(Type name);
    }
}