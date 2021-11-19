using Networking.EventArgs;

namespace Networking
{
    public delegate void MessageReceivedEventHandler(Client sender, MessageEventArgs args);

    public delegate void MessageReceivedEventHandler<T>(Client sender, MessageEventArgs<T> args);
}