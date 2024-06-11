namespace Client.Common.Services.UIService.Events
{
    public readonly struct WindowInitEvent
    {
        public IUIWindow Window { get; }
        public WindowInitEvent(IUIWindow window)
        {
            Window = window;
        }
    }
}