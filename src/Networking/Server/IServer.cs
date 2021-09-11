using System;
using System.Collections.Generic;

namespace Networking
{
    public interface IServer : IDisposable
    {
        public void Open();

        public void Close();
        
        public IList<Client> ConnectedClients { get; }

        /// <summary>
        /// Sends asynchronous message.
        /// </summary>
        public void SendMessage(Client connection, byte[] message);

        /// <summary>
        /// Sends request and waits for response.
        /// </summary>
        public byte[] SendRequest(Client connection, byte[] request);

        /// <summary>
        /// Broadcasts a message to all clients.
        /// </summary>
        public void Broadcast(byte[] message);

        /// <summary>
        /// Fired when a device connects.
        /// </summary>
        public event ClientConnectedEventHandler ClientConnected;
        
        /// <summary>
        /// Fired when a message is received.
        /// </summary>
        public event MessageReceivedEventHandler MessageReceived;

        /// <summary>
        /// Fired when a request is received.
        /// </summary>
        public event RequestReceivedEventHandler RequestReceived;
    }
}
