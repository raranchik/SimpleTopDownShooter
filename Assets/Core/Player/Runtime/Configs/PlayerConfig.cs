using System;
using Newtonsoft.Json;

namespace Core.Player.Configs
{
    [Serializable, JsonObject(MemberSerialization.Fields)]
    public class PlayerConfig
    {
        [JsonProperty("movement")] private PlayerMovementConfig m_Movement;

        public PlayerMovementConfig Movement => m_Movement;
    }
}