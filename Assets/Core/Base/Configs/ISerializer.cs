using System;

namespace Core.Base.Configs
{
    public interface ISerializer
    {
        string Serialize(object target);
        T Deserialize<T>(string data);
        object Deserialize(string data, Type type);
    }
}