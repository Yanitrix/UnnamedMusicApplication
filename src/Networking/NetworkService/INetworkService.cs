using System;

namespace Networking
{
    public interface INetworkService : IDisposable
    {
        void OpenConnection();
        void CloseConnection();
        string ConnectionToken { get; }
        int ConnectionCount { get; }

        event DeviceConnectedEventHandler DeviceConnected;
        event DeviceDisconnectedEventHandler DeviceDisconnected;

        IReceiver Receiver { get; }
        ISender Sender { get; }
    }
}