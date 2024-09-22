using System;
using Newtonsoft.Json;

namespace Core.Zones
{
    [Serializable, JsonObject(MemberSerialization.Fields)]
    public class ZonesGeneralConfig
    {
        [JsonProperty("zones_spacing")] private float m_ZonesSpacing;
        [JsonProperty("field_margin")] private float m_FieldMargin;

        public float ZonesSpacing => m_ZonesSpacing;
        public float FieldMargin => m_FieldMargin;
    }
}