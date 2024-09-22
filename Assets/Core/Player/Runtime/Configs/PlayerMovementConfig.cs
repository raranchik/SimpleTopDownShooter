using System;
using Newtonsoft.Json;

namespace Core.Player.Configs
{
    [Serializable, JsonObject(MemberSerialization.Fields)]
    public class PlayerMovementConfig
    {
        [JsonProperty("move_speed")] private float m_MoveSpeed;
        [JsonProperty("rotation_speed")] private float m_RotationSpeed;

        public float MoveSpeed => m_MoveSpeed;
        public float RotationSpeed => m_RotationSpeed;
    }
}