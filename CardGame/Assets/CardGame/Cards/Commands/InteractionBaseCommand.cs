using System.Threading.Tasks;
using CardGame.Services.CommandService;

namespace CardGame.Cards
{
    public abstract class InteractionBaseCommand : ICommand<InteractionProtocol>
    {
        public abstract Task Execute(InteractionProtocol protocol);
    }
}