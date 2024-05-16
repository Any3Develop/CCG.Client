using Client.Lobby.Runtime.Data;

namespace Client.Lobby.Runtime
{
    public static class User
    {
        public static string Id => RuntimeData.Id;
        public static string AccessToken => RuntimeData.AccessToken;
        public static string UserName => RuntimeData.UserName;
        public static RuntimeUserData RuntimeData { get; private set; }

        static User()
        {
            RuntimeData = new RuntimeUserData();
        }
        
        public static void Sync(RuntimeUserData runtimeData)
        {
            RuntimeData = runtimeData;
        }
    }
}