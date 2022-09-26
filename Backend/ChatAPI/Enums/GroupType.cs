using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ChatAPI.Enums
{

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum GroupType
    {
        [EnumMember(Value = nameof(Global))]
        Global,

        [EnumMember(Value = nameof(Entertainment))]
        Entertainment,

        [EnumMember(Value = nameof(VideoGames))]
        VideoGames,

        [EnumMember(Value = nameof(All))]
        All
    }
}
