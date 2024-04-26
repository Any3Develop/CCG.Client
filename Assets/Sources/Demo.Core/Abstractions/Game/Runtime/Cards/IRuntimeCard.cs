using Demo.Core.Abstractions.Game.Runtime.Common;
using Demo.Core.Abstractions.Game.Runtime.Data;
using Demo.Core.Game.Data;

namespace Demo.Core.Abstractions.Game.Runtime.Cards
{
    public interface IRuntimeCard : IRuntimeObject
    {
        new CardData Data { get;}
        new IRuntimeCardData RuntimeData { get; }

        void SetPosition(int? value, bool notify = true);
    }
}