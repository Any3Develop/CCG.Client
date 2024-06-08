using System;
using System.Linq;

namespace Shared.Game.Utils
{
    public static class StringExtensions
    {
        /// <summary>
        /// Will format any public fields and their values into a string.
        /// </summary>
        public static string ReflectionFormat(this object source)
        {
            var refType = source?.GetType();
            return refType == null
                ? $"FormatUtils.ReflectionFormat : {nameof(NullReferenceException)}"
                : $"[ReflectedType : {refType.Name}]\n" + string.Join("\n", refType.GetFields().Select(x => $"[{x.Name} : {x.GetValue(source)}]")) + "\n"
                  + string.Join("\n", refType.GetProperties().Select(x => $"[{x.Name} : {x.GetValue(source)}]"));
        }
    }
}