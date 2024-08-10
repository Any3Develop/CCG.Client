namespace Services.UIService.Abstractions
{
    public interface IUIAnimationsFactory
    {
        IUIAnimation Create(IUIWindow window, IUIAnimationConfig config);
    }
}