namespace Client.Services.UIService
{
    public interface IUIAudioConfig
    {
        public bool EnabledByDefault { get; }
        public bool ReInitWhenModified { get; }
    }
}