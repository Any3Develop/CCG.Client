namespace Client.Services.UIService
{
    public interface IUIAudioSourceFactory
    {
        IUIAudioSource Create(IUIWindow window);
    }
}