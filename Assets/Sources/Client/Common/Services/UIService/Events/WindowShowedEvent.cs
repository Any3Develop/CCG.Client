namespace Client.Common.Services.UIService.Events
{
    public readonly struct WindowShowedEvent
    {
        public IUIWindow Window { get; }
        public WindowShowedEvent(IUIWindow window)
        {
            Window = window;
        }
    }
}