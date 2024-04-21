using CardGame.Services.BootstrapService;
using CardGame.Session;
using Zenject;

namespace CardGame.App
{
    public class StartGameCommand : Command
    {
        private readonly IInstantiator _instantiator;

        public StartGameCommand(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public override void Do()
        {
            _instantiator.Instantiate<CreateSessionCommand>()
                .Execute(new CreateSessionProtocol(null));
            OnDone();
        }
    }
}