namespace Client.Common.Services.UIService.Events
{
    public readonly struct WindowInitEvent
    {
        public IWindow Window { get; }
        public WindowInitEvent(IWindow window)
        {
            Window = window;
        }
    }
}