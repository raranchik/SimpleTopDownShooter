using System;
using Newtonsoft.Json;

namespace Core.Base.Configs
{
    public class NewtonsoftSerializer : ISerializer
    {
        public string Serialize(object target)
        {
            return JsonConvert.SerializeObject(target);
        }

        public T Deserialize<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }

        public object Deserialize(string data, Type type)
        {
            return JsonConvert.DeserializeObject(data, type);
        }
    }
}