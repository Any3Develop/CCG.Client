using System.Threading.Tasks;

namespace CardGame.Cards
{
    public class SceneObjectInteractionCommand : InteractionBaseCommand
    {
        public override Task Execute(InteractionProtocol protocol)
        {
            return Task.CompletedTask;
        }
    }
}