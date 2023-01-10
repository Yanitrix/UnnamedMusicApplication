using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Networking
{
    //i guess we're gonna need asynchronous events handling in this class
    public interface IServer : IDisposable
    {
        public void Open();

        public void Close();

        public IList<Client> ConnectedClients { get; }

        /// <summary>
        /// Sends asynchronous message.
        /// </summary>
        public Task SendMessageAsync(Client connection, byte[] message);

        /// <summary>
        /// Sends request and waits for response.
        /// </summary>
        public Task<byte[]> SendRequestAsync(Client connection, byte[] request);

        /// <summary>
        /// Broadcasts a message to all clients.
        /// </summary>
        public Task BroadcastAsync(byte[] message);

        public event Action<Client> ClientConnected;

        public event Action<Client> ClientDisconnected;

        /// <summary>
        /// Fired when a request is received.
        /// </summary>
        public event RequestReceivedEventHandler RequestReceived;
    }
}
