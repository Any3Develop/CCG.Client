using System;
using System.Linq;

namespace Shared.Game.Utils
{
    public static class MapExtensions
    {
        public static T Map<T>(this T destination, object from)
        {
            if (destination == null)
                throw new NullReferenceException($"{nameof(Map)} cannot executed, {nameof(destination)} '{typeof(T).Name}' object is null");

            if (from == null)
                throw new NullReferenceException($"{nameof(Map)} cannot executed, '{nameof(from)}' object is null");

            var fromFields = from.GetType().GetFields().ToDictionary(x => x.Name);
            var fromProperties = from.GetType().GetProperties().Where(x=> x.CanRead).ToDictionary(x => x.Name);
            
            var destinationFields = destination.GetType().GetFields();
            var destinationProperties = destination.GetType().GetProperties().Where(x=> x.CanWrite).ToArray();
            
            foreach (var destinationField in destinationFields)
            {
                if (fromFields.TryGetValue(destinationField.Name, out var fromFieldInfo)
                    && destinationField.FieldType == fromFieldInfo.FieldType)
                    destinationField.SetValue(destination, fromFieldInfo.GetValue(from));
            }

            foreach (var destinationProperty in destinationProperties)
            {
                if (fromProperties.TryGetValue(destinationProperty.Name, out var fromPropertyInfo)
                    && destinationProperty.PropertyType == fromPropertyInfo.PropertyType)
                    destinationProperty.SetValue(destination, fromPropertyInfo.GetValue(from));
            }

            return destination;
        }

        public static bool ValueEquals(this object left, object rigth)
        {
            try
            {
                if (left == null && rigth == null)
                    return true;
            
                if (left == null || rigth == null)
                    return false;
                
                var rigthType = rigth.GetType();
                var leftType = left.GetType();
                
                return leftType.GetFields().All(x => x.GetValue(left).Equals(rigthType.GetField(x.Name)?.GetValue(rigth)))
                       && leftType.GetProperties().All(x => x.GetValue(left).Equals(rigthType.GetProperty(x.Name)?.GetValue(rigth)));
            }
            catch
            {
                return false;
            }
        }
    }
}