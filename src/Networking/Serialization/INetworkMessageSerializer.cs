using System;

namespace Networking.Serialization
{
    public interface INetworkMessageSerializer
    {
        public T Deserialize<T>(byte[] data);

        public object Deserialize(Type type, byte[] data);

        public byte[] Serialize<T>(T message);
    }
}