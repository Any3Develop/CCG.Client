namespace Shared.Abstractions.Game.Commands
{
    
    public interface ICommandModel
    {
        string TypeName { get; }
        string CommandId { get; }
        string PredictionId { get; set; }
        bool IsNested { get; }
    }
}