using System;
using Shared.Abstractions.Game.Runtime.Data;

namespace Shared.Abstractions.Game.Context.Providers
{
    public interface IRuntimeIdProvider : IDisposable
    {
        IRuntimeIdData RuntimeData { get; }
        void Sync(IRuntimeIdData runtimeData);
        int Next();
    }
}