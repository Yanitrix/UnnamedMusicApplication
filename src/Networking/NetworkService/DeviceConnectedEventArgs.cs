namespace Networking
{
    public class DeviceConnectedEventArgs : System.EventArgs
    {
        public DeviceConnectedEventArgs(ConnectionRequest connectionRequest, ConnectionResponse response)
        {
            ConnectionRequest = connectionRequest;
            Response = response;
        }

        public ConnectionRequest ConnectionRequest { get; }
        public ConnectionResponse Response { get; }
        public bool Accepted { get; set; }
    }
}