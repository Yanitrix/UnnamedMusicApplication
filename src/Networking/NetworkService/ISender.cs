using Networking.Messages.Server;
using Networking.Serialization;
using System;
using System.Threading.Tasks;

namespace Networking
{
    public interface ISender : IDisposable
    {
        //should it return response? for now it'll just throw an exception
        public Task QueueUpdated(QueueUpdatedMessage msg);

        public Task CurrentSongSet(SetCurrentSongMessage msg);

        public Task ShuffleChanged(ShuffleChangedMessage msg);

        public Task QueueMoved(QueueMovedMessage msg);
    }

    public class Sender : ISender
    {
        private bool disposed;

        private readonly IServer server;
        private readonly INetworkMessageSerializer serializer;

        public Sender(IServer server, INetworkMessageSerializer serializer)
        {
            this.server = server;
            this.serializer = serializer;
        }

        public Task CurrentSongSet(SetCurrentSongMessage msg) => SerializeAndSend(msg);

        public Task QueueMoved(QueueMovedMessage msg) => SerializeAndSend(msg);

        public Task QueueUpdated(QueueUpdatedMessage msg) => SerializeAndSend(msg);

        public Task ShuffleChanged(ShuffleChangedMessage msg) => SerializeAndSend(msg);

        //TODO test
        private async Task SerializeAndSend<T>(T message)
        {
            var code = MessageCodes.GetCode<T>();
            var codeBytes = new byte[]{ code };
            var bytes = serializer.Serialize(message);
            byte[] response = new byte[bytes.Length + 1]; //additional byte for message type

            var index = bytes.Length;
            Buffer.BlockCopy(bytes, 0, response, 0, index);
            Buffer.BlockCopy(codeBytes, 0, response, index, 1);

            await server.BroadcastAsync(response).ConfigureAwait(false);
        }

        public void Dispose()
        {
            if (!disposed)
            {
                server.Dispose();
                disposed = true;
            }

            GC.SuppressFinalize(this);
        }
    }

    internal class MessageCodes
    {
        public static byte GetCode<T>()
        {
            throw new NotImplementedException();
        }
    }
}