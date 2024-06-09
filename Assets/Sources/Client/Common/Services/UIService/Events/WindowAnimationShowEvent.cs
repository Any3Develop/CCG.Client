namespace Client.Common.Services.UIService.Events
{
    public readonly struct WindowAnimationShowEvent
    {
        public IUIAnimation Animation { get; }
        public IWindow Window { get; }

        public WindowAnimationShowEvent(IWindow window, IUIAnimation animation)
        {
            Window = window;
            Animation = animation;
        }
    }
}