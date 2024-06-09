namespace Client.Common.Services.UIService.Events
{
    public readonly struct WindowHideEvent
    {
        public IWindow Window { get; }
        public WindowHideEvent(IWindow window)
        {
            Window = window;
        }
    }
}