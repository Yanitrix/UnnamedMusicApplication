using System;

namespace Networking
{
    public class MessageEventArgs : System.EventArgs
    {
        public byte[] Data { get; init; }
    }
}