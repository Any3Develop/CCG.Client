using System;
using Shared.Game.Data.Enums;

namespace Shared.Game.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class EffectIdAttribute : Attribute
    {
        public Keyword Value;

        public EffectIdAttribute(Keyword value)
        {
            Value = value;
        }
    }
}