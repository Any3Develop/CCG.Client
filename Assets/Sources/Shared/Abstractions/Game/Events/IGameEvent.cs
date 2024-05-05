namespace Shared.Abstractions.Game.Events
{
    public interface IGameEvent
    {
        int Order { get; }
        string PredictionId { get; }
        bool Rollback { get; }
    }
}