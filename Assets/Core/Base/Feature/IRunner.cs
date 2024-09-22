using System;
using System.Collections;

namespace Core.Base.Feature
{
    public interface IRunner
    {
        IRunner AddSystem(ISystem system);
        IEnumerator Initialize();
        ISystem GetSystem(Type name);
        void SortFeaturesOnEachSystem();
    }
}