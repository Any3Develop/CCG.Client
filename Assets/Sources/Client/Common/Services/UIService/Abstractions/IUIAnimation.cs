using Cysharp.Threading.Tasks;

namespace Client.Common.Services.UIService
{
    public interface IUIAnimation
    {
        void Init(IUIWindow window);
        UniTask PlayAsync(bool forceEnd = false);
        UniTask StopAsync(bool forceEnd = false);
        void Restart();
    }
}