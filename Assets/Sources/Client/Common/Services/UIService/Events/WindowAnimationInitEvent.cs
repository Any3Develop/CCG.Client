namespace Client.Common.Services.UIService.Events
{
    public readonly struct WindowAnimationInitEvent
    {
        public IUIAnimation Animation { get; }
        public IUIWindow Window { get; }

        public WindowAnimationInitEvent(IUIWindow window, IUIAnimation animation)
        {
            Window = window;
            Animation = animation;
        }
    }
}