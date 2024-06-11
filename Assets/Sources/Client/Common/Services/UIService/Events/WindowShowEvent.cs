namespace Client.Common.Services.UIService.Events
{
    public readonly struct WindowShowEvent
    {
        public IUIWindow Window { get; }
        public WindowShowEvent(IUIWindow window)
        {
            Window = window;
        }
    }
}