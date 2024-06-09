using Cysharp.Threading.Tasks;

namespace Client.Common.Services.UIService
{
    public interface IUIAnimation
    {
        UniTask PlayAsync();
        UniTask StopAsync();
        void Reset();
    }
}