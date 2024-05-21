using Shared.Abstractions.Game.Commands;

namespace Shared.Game.Commands.Base
{
    public class CommandModelBase : ICommandModel
    {
        public string TypeName { get; set; }
        public string CommandId { get; set; }
        public string PredictionId { get; set; }
        public bool IsNested { get; set; }
    }
}