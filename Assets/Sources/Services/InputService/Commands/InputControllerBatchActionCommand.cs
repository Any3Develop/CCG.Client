using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardGame.Services.CommandService;

namespace CardGame.Services.InputService
{
    public class InputControllerBatchActionCommand : ICommand<InputControllerBatchActionProtocol>
    {
        private readonly IEnumerable<IInputController<IInputLayer>> _inputControllers;

        public InputControllerBatchActionCommand(IEnumerable<IInputController<IInputLayer>> inputControllers)
        {
            _inputControllers = inputControllers;
        }
        public Task Execute(InputControllerBatchActionProtocol protocol)
        {
            foreach (var type in protocol.Composite)
            {
                var controller = _inputControllers.FirstOrDefault(x => x.InputLayer == type);
                if (controller != null)
                {
                    if (protocol.Value)
                    {
                        controller.Lock();
                    }
                    else
                    {
                        controller.Unlock();
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}