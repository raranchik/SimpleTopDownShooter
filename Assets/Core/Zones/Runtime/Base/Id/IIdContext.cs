using Core.Base;
using UnityEngine;

namespace Core.Zones.Base.Id
{
    public interface IIdContext
    {
        Result<string> GetId(GameObject zoneObject);
    }
}