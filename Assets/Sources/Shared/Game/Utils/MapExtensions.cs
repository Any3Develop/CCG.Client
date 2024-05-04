using System;
using System.Linq;

namespace Shared.Game.Utils
{
    public static class MapExtensions
    {
        public static T Map<T>(this T destination, object from)
        {
            if (from == null || destination == null)
                throw new NullReferenceException($"FillFrom cannot executed, target or source object is null");

            var destinationFields = destination.GetType().GetFields();
            var destinationProperties = destination.GetType().GetProperties();
            var sourceFields = from.GetType().GetFields().ToDictionary(x => x.Name);
            var sourceProperties = from.GetType().GetProperties().ToDictionary(x => x.Name);

            foreach (var destinationField in destinationFields)
            {
                if (sourceFields.TryGetValue(destinationField.Name, out var fieldInfo)
                    && fieldInfo.FieldType == destinationField.FieldType)
                    destinationField.SetValue(destination, fieldInfo.GetValue(from));
            }

            foreach (var destinationProperty in destinationProperties)
            {
                if (destinationProperty.CanWrite 
                    && sourceProperties.TryGetValue(destinationProperty.Name, out var propertyInfo)
                    && propertyInfo.PropertyType == destinationProperty.PropertyType)
                    destinationProperty.SetValue(destination, propertyInfo.GetValue(from));
            }

            return destination;
        }
    }
}