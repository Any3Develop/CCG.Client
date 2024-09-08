namespace Shared.Abstractions.Common.Network
{
    public interface INetworkSerializer
    {
        byte[] Serialize(object data);
        T Deserialize<T>(byte[] data);
        object Deserialize(byte[] data);
    }
}