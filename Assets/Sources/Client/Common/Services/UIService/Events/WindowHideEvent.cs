namespace Client.Common.Services.UIService.Events
{
    public readonly struct WindowHideEvent
    {
        public IUIWindow Window { get; }
        public WindowHideEvent(IUIWindow window)
        {
            Window = window;
        }
    }
}