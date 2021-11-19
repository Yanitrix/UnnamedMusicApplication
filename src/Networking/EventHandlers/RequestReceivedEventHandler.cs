using Networking.EventArgs;

namespace Networking
{
    public delegate void RequestReceivedEventHandler(Client sender, RequestEventArgs args);

    public delegate void RequestReceivedEventHandler<TRequest, TResponse>
        (
        Client client,
        RequestEventArgs<TRequest, TResponse> eventArgs
        );
}