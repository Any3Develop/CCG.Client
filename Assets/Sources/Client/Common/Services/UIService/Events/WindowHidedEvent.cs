namespace Client.Common.Services.UIService.Events
{
    public readonly struct WindowHidedEvent
    {
        public IUIWindow Window { get; }
        public WindowHidedEvent(IUIWindow window)
        {
            Window = window;
        }
    }
}