using Newtonsoft.Json;
using Unity.Plastic.Newtonsoft.Json.Converters;

namespace Shared.Common.Network
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Route
    {
        Auth = 0,
        ClientInit,
        GameEvent,
        Command,
        StartSession,
    }
}