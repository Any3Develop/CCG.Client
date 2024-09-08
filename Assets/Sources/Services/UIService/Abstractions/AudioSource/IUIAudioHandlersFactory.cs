namespace Client.Services.UIService
{
    public interface IUIAudioHandlersFactory
    {
        IUIAudioHandler Create(IUIWindow window, IUIAudioConfig config);
    }
}