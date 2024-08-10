namespace Services.UIService.Abstractions
{
    public interface IUIAudioHandlersFactory
    {
        IUIAudioHandler Create(IUIWindow window, IUIAudioConfig config);
    }
}