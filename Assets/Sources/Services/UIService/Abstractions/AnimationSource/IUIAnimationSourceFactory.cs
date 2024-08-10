namespace Services.UIService.Abstractions
{
    public interface IUIAnimationSourceFactory
    {
        IUIAnimationSource Create(IUIWindow window);
    }
}