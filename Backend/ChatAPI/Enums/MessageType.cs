using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ChatAPI.Enums
{

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MessageType
    {
        [EnumMember(Value = nameof(Message))]
        Message,

        [EnumMember(Value = nameof(JoinInformation))]
        JoinInformation,

        [EnumMember(Value = nameof(ExitInformation))]
        ExitInformation
    }
}
