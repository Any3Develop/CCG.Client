using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Shared.Common.Logger;

namespace Shared.Game.Utils
{
    public static class ExtensionMethods
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
        
        public static bool CompareValues<T>(this T value1, T value2) // T is contract to equal type
        {
            try
            {
                if (value1 == null && value2 == null)
                    return true;
                
                if (value1 == null || value2 == null)
                    return false;
                
                var type = value1.GetType(); // get the specific type
                const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
                
                return type.GetFields(flags).All(x => Compare(x.GetValue(value1), x.GetValue(value2))) 
                       && type.GetProperties(flags).All(x => Compare(x.GetValue(value1), x.GetValue(value2)));
            }
            catch (Exception e)
            {
                SharedLogger.Error(e);
                return false;
            }
        }

        private static bool Compare(object value1, object value2)
        {
            if (value1 == null && value2 == null)
                return true;

            if (value1 == null || value2 == null)
                return false;
            
            if (value1 is IEnumerable collection1 && value2 is IEnumerable collection2)
                return Compare(collection1, collection2);

            return value1.Equals(value2) || (value1.GetType().IsClass && CompareValues(value1, value2));
        }

        private static bool Compare(IEnumerable collection1, IEnumerable collection2)
        {
            if (collection1 == null && collection2 == null)
                return true;

            if (collection1 == null || collection2 == null)
                return false;

            var enumerator1 = collection1.GetEnumerator();
            var enumerator2 = collection2.GetEnumerator();
            
            while (true)
            {
                var move1 = enumerator1.MoveNext();
                var move2 = enumerator2.MoveNext();
                
                if (!move1 && !move2)
                    return true;
                
                if (move1 != move2)
                    return false;
                
                if (!CompareValues(enumerator1.Current, enumerator2.Current))
                    return false;
            }
        }
    }
}