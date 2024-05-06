namespace Shared.Abstractions.Game.Events
{
    public interface IGameEvent
    {
        int Order { get; set; }
        string PredictionId { get; }
        bool Rollback { get; }
    }
}