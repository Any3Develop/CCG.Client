namespace Client.Common.Services.UIService.Events
{
    public readonly struct WindowAnimationStopEvent
    {
        public IUIAnimation Animation { get; }
        public IUIWindow Window { get; }

        public WindowAnimationStopEvent(IUIWindow window, IUIAnimation animation)
        {
            Window = window;
            Animation = animation;
        }
    }
}