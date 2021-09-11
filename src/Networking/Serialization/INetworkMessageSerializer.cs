using System;

namespace Networking.Serialization
{
    public interface INetworkMessageSerializer
    {
        public T Deserialize<T>(byte[] data, Type messageType);

        public byte[] Serialize<T>(T message);
    }
}