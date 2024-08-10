namespace Services.UIService.Abstractions
{
    public interface IUIAudioSourceFactory
    {
        IUIAudioSource Create(IUIWindow window);
    }
}