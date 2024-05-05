using System;
using Shared.Abstractions.Game.Context;

namespace Shared.Game.Context
{
    public class SharedTime : ISharedTime
    {
        public DateTime Current => DateTime.UtcNow;
    }
}