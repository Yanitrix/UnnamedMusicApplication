using Networking.Messages;

namespace Networking.EventArgs
{
    public class RequestEventArgs<TRequest, TResponse> : System.EventArgs
    {
        public RequestEventArgs(TRequest request)
        {
            Request = request;
        }

        public TRequest Request { get; set; }

        public Response<TResponse> Response { get; set; }
    }
}
