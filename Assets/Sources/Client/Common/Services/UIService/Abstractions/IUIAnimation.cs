using Cysharp.Threading.Tasks;

namespace Client.Common.Services.UIService
{
    public interface IUIAnimation
    {
        void Enable();
        void Disable();
        UniTask PlayAsync();
        UniTask ResetAsync();
    }
}