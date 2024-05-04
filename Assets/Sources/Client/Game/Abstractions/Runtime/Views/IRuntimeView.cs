using System;
using Client.Game.Abstractions.Runtime.Models;

namespace Client.Game.Abstractions.Runtime.Views
{
    public interface IRuntimeView : IDisposable
    {
        IRuntimeModel Model { get; }
        void Setup(IRuntimeModel model);
    }
}