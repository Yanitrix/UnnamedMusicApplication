using Networking.Serialization;
using System;
using System.Text;
using System.Text.Json;

namespace Networking.Tests
{
    internal class MessageSerializerFake : INetworkMessageSerializer
    {
        public T Deserialize<T>(byte[] data)
        {
            var json = Encoding.UTF8.GetString(data);
            return JsonSerializer.Deserialize<T>(json);
        }

        public object Deserialize(Type type, byte[] data)
        {
            var json = Encoding.UTF8.GetString(data);
            return JsonSerializer.Deserialize(json, type);
        }

        public byte[] Serialize<T>(T message)
        {
            var json = JsonSerializer.Serialize(message);
            return Encoding.UTF8.GetBytes(json);
        }
        //TODO move Networking.Tests from /src to /tests and edit .sln file 
    }
}
