using Demo.Core.Abstractions.Game.Commands;

namespace Demo.Core.Game.Commands.Base
{
    public class CommandModelBase : ICommandModel
    {
        public string CommandId { get; set; }
    }
}