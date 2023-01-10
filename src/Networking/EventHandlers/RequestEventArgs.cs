using System;

namespace Networking
{
    public class RequestEventArgs : EventArgs
    {
        public byte[] RequestData { get; init; }
        public byte[] ResponseData { get; set; }
    }
}