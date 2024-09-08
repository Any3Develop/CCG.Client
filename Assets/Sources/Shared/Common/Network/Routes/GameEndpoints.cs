using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Shared.Common.Network.Routes
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GameEndpoints
    {
        CloseConnection,
        ExecuteCommand,
        GameEvent,
        Error,
        Ping,
    }
}