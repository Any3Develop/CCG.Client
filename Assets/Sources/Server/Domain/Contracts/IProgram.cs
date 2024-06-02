using System;
using System.Threading.Tasks;

namespace Server.Domain.Contracts
{
    public interface IProgram : IDisposable
    {
        Task Main();
    }
}