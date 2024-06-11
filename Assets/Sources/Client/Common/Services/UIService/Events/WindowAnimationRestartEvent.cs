namespace Client.Common.Services.UIService.Events
{
    public readonly struct WindowAnimationRestartEvent
    {
        public IUIAnimation Animation { get; }
        public IUIWindow Window { get; }

        public WindowAnimationRestartEvent(IUIWindow window, IUIAnimation animation)
        {
            Window = window;
            Animation = animation;
        }
    }
}