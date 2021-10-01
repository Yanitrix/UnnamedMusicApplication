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
        event PlaylistsChangedEventHandler PlaylistsChanged;
        event QueueChangedEventHandler QueueChanged;
    }
}