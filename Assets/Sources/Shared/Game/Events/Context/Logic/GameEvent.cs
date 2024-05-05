using Newtonsoft.Json;
using Shared.Abstractions.Game.Events;

namespace Shared.Game.Events.Context.Logic
{
    public abstract class GameEvent : IGameEvent
    {
        public int Order { get; set; }
        public string PredictionId { get; set; }
        [JsonIgnore] public bool Reconciled { get; set; }
    }
}