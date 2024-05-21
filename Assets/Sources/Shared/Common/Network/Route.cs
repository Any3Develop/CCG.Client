using Newtonsoft.Json;
using Unity.Plastic.Newtonsoft.Json.Converters;

namespace Shared.Common.Network
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Route
    {
        DropConnection,
        Auth,
        GameEvent,
        Command
    }
}