namespace Client.Services.UIService
{
    public interface IUIAnimationSourceFactory
    {
        IUIAnimationSource Create(IUIWindow window);
    }
}