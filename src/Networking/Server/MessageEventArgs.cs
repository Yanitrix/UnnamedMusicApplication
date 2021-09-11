using System;

namespace Networking
{
    public class MessageEventArgs : EventArgs
    {
        public byte[] Data { get; init; }
    }
}