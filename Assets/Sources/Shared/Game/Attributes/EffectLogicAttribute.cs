using System;
using Shared.Game.Data.Enums;

namespace Shared.Game.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class EffectLogicAttribute : Attribute
    {
        public LogicId Value { get; }

        public EffectLogicAttribute(LogicId value)
        {
            Value = value;
        }
    }
}