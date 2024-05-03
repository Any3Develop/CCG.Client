using Shared.Abstractions.Game.Runtime.Data;
using Shared.Abstractions.Game.Runtime.Objects;
using Shared.Game.Data;

namespace Shared.Abstractions.Game.Runtime.Cards
{
    public interface IRuntimeCard : IRuntimeObject
    {
        new CardData Data { get;}
        new IRuntimeCardData RuntimeData { get; }

        void SetPosition(int? value, bool notify = true);
    }
}