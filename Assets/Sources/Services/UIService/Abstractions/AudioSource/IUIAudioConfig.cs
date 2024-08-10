namespace Services.UIService.Abstractions
{
    public interface IUIAudioConfig
    {
        public bool EnabledByDefault { get; }
        public bool ReInitWhenModified { get; }
    }
}