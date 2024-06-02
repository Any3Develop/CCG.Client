using Server.Domain.Contracts;
using Zenject;

namespace Startup.Infrastructure.Server
{
    public class ApiLauncher : IInitializable
    {
        private readonly IProgram program;
        public ApiLauncher(IProgram program)
        {
            this.program = program;
        }

        public void Initialize()
        {
            program.Main().ConfigureAwait(false).GetAwaiter();
        }
    }
}