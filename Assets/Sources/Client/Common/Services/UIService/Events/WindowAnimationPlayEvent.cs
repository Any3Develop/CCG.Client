namespace Client.Common.Services.UIService.Events
{
    public readonly struct WindowAnimationPlayEvent
    {
        public IUIAnimation Animation { get; }
        public IUIWindow Window { get; }

        public WindowAnimationPlayEvent(IUIWindow window, IUIAnimation animation)
        {
            Window = window;
            Animation = animation;
        }
    }
}