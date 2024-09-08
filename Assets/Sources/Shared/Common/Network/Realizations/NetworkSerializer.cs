using System.Text;
using Newtonsoft.Json;
using Shared.Abstractions.Common.Network;
using Shared.Game.Utils;

namespace Shared.Common.Network
{
    public class NetworkSerializer : INetworkSerializer
    {
        public byte[] Serialize(object data)
        {
            var jsonData = data as string ?? JsonConvert.SerializeObject(data, SerializeExtensions.SerializeSettings);
            return Encoding.UTF8.GetBytes(jsonData);
        }

        public T Deserialize<T>(byte[] data)
        {
            var jsonData = GetJsonFromBytes(data);
            return JsonConvert.DeserializeObject<T>(jsonData, SerializeExtensions.GetDeserializeSettingsByType<T>());
        }

        public object Deserialize(byte[] data)
        {
            var jsonData = GetJsonFromBytes(data);
            return JsonConvert.DeserializeObject(jsonData, SerializeExtensions.DeserializeSettings);
        }

        private static string GetJsonFromBytes(byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }
    }
}