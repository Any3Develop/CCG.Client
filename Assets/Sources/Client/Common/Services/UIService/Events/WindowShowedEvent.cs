namespace Client.Common.Services.UIService.Events
{
    public readonly struct WindowShowedEvent
    {
        public IWindow Window { get; }
        public WindowShowedEvent(IWindow window)
        {
            Window = window;
        }
    }
}