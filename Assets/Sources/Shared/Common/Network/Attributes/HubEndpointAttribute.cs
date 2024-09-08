using System;

namespace Shared.Common.Network.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HubEndpointAttribute : Attribute
    {
        public string Target { get; }

        public HubEndpointAttribute(object target)
        {
            Target = target?.ToString().ToLower();
        }
    }
}