using System;

namespace Shared.Abstractions.Game.Context
{
    public interface ISharedTime
    {
        DateTime Current { get; }
    }
}