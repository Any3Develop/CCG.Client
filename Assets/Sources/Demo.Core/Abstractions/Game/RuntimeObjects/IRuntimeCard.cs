using Demo.Core.Abstractions.Game.RuntimeData;
using Demo.Core.Game.Data;

namespace Demo.Core.Abstractions.Game.RuntimeObjects
{
    public interface IRuntimeCard : IRuntimeObject
    {
        new CardData Data { get;}
        new IRuntimeCardData RuntimeData { get; }

        void SetPosition(int? value, bool notify = true);
    }
}