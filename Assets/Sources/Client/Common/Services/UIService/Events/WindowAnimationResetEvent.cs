namespace Client.Common.Services.UIService.Events
{
    public readonly struct WindowAnimationResetEvent
    {
        public IUIAnimation Animation { get; }
        public IUIWindow Window { get; }

        public WindowAnimationResetEvent(IUIWindow window, IUIAnimation animation)
        {
            Window = window;
            Animation = animation;
        }
    }
}