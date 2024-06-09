namespace Client.Common.Services.UIService.Events
{
    public readonly struct WindowAnimationHideEvent
    {
        public IUIAnimation Animation { get; }
        public IWindow Window { get; }

        public WindowAnimationHideEvent(IWindow window, IUIAnimation animation)
        {
            Window = window;
            Animation = animation;
        }
    }
}