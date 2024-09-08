namespace Client.Services.UIService
{
    public interface IUIAnimationsFactory
    {
        IUIAnimation Create(IUIWindow window, IUIAnimationConfig config);
    }
}