namespace Client.Common.Services.UIService.Events
{
    public readonly struct WindowHidedEvent
    {
        public IWindow Window { get; }
        public WindowHidedEvent(IWindow window)
        {
            Window = window;
        }
    }
}