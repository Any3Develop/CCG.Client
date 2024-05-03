using Shared.Abstractions.Game.Commands;

namespace Shared.Game.Commands.Base
{
    public class CommandModelBase : ICommandModel
    {
        public string CommandId { get; set; }
    }
}