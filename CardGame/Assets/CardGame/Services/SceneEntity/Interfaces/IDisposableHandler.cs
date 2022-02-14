namespace CardGame.Services.SceneEntity
{
    /// <summary>
    /// Will be processed before deletion
    /// </summary>
    public interface IDisposableHandler
    {
        void OnDisposeHandle();
    }
}