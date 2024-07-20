using System.Collections;
using System.Collections.Generic;

namespace Core.Base.Feature
{
    public interface ILauncher
    {
        IEnumerator InitializeSystems(IEnumerable<ISystem> systems);
    }
}