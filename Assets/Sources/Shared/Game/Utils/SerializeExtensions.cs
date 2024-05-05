using System;
using Newtonsoft.Json;

namespace Shared.Game.Utils
{
    public static class SerializeExtensions
    {
        public static T Clone<T>(this T instance)
        {
            if (instance == null)
                return default;

            var jsonData = JsonConvert.SerializeObject(instance, SerializeSettings);
            return JsonConvert.DeserializeObject<T>(jsonData, GetDeserializeSettingsByType<T>());
        }

        private static ISharedSerializationBinder SerializationBinder { get; } = new SharedSerializationBinder();

        public static JsonSerializerSettings DeserializeSettings { get; } = new()
        {
            TypeNameHandling = TypeNameHandling.Objects,
            SerializationBinder = SerializationBinder
        };

        public static JsonSerializerSettings SerializeSettings { get; } = new()
        {
            TypeNameHandling = TypeNameHandling.Objects
        };

        public static JsonSerializerSettings GetDeserializeSettingsByType<T>()
        {
            return GetDeserializeSettingsByType(typeof(T));
        }

        public static JsonSerializerSettings GetDeserializeSettingsByType(Type targetType)
        {
            if (targetType.IsGenericParameter)
                targetType = targetType.GetGenericTypeDefinition();

            return IsSupportsWithSerializeSettings(targetType) ? DeserializeSettings : default;
        }

        public static bool IsSupportsWithSerializeSettings(Type type)
        {
            return type?.Assembly.FullName.Contains(SerializationBinder.TargetAssemblyName) ?? false;
        }
    }
}