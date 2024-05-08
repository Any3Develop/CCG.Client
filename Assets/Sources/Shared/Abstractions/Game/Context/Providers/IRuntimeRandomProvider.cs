using System;
using Shared.Abstractions.Game.Runtime.Data;

namespace Shared.Abstractions.Game.Context.Providers
{
    public interface IRuntimeRandomProvider : IDisposable
    {
        IRuntimeRandomData RuntimeData { get; }
        void Sync(IRuntimeRandomData runtimeData);
        int Next();
    }
}