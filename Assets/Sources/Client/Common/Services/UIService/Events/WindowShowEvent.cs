namespace Client.Common.Services.UIService.Events
{
    public readonly struct WindowShowEvent
    {
        public IWindow Window { get; }
        public WindowShowEvent(IWindow window)
        {
            Window = window;
        }
    }
}