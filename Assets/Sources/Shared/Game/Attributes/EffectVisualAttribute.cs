using System;
using Shared.Game.Data.Enums;

namespace Shared.Game.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class EffectVisualAttribute : Attribute
    {
        public VisualId Value { get; }

        public EffectVisualAttribute(VisualId value)
        {
            Value = value;
        }
    }
}