using Networking.Messages;

namespace Networking.EventArgs
{
    public class MessageEventArgs<TMessage> : System.EventArgs
    {
        public MessageEventArgs(TMessage message)
        {
            Message = message;
            Response = Response.Succeed();
        }

        public TMessage Message { get; }

        public Response Response { get; set; }
    }
}
