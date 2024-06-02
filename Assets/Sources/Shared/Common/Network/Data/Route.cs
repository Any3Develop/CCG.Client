using Newtonsoft.Json;
using Unity.Plastic.Newtonsoft.Json.Converters;

namespace Shared.Common.Network.Data
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Route
    {
        DropConnection,
        Config,
        Database,
        Auth,
        GameEvent,
        Command
    }
}