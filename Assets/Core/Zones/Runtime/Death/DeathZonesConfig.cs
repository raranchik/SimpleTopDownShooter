using System;
using Newtonsoft.Json;

namespace Core.Zones.Death
{
    [Serializable, JsonObject(MemberSerialization.Fields)]
    public class DeathZonesConfig
    {
        [JsonProperty("count_on_field")] private int m_CountOnField;
        [JsonProperty("radius")] private float m_Radius;

        [JsonProperty("death_with_invincibility")]
        private float m_DeathWithInvincibility;

        public int CountOnField => m_CountOnField;
        public float Radius => m_Radius;
        public float DeathWithInvincibility => m_DeathWithInvincibility;
    }
}