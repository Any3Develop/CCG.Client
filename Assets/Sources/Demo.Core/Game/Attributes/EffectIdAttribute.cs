using System;
using Demo.Core.Game.Data.Enums;

namespace Demo.Core.Game.Attributes
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