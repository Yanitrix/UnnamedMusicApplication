using System;

namespace Networking
{
    public class RequestEventArgs : System.EventArgs
    {
        public byte[] RequestData { get; init; }
        public byte[] ResponseData { get; set; }
    }
}