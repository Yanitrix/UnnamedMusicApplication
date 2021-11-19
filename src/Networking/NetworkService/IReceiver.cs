using Networking.EventArgs;
using Networking.Messages;
using Networking.Messages.Client;
using Networking.Messages.Server;
using Networking.NetworkService;
using System;

namespace Networking
{
    public interface IReceiver : IDisposable
    {
        public event MessageReceivedEventHandler<SuggestionMessage> SuggestionReceived;

        public event MessageReceivedEventHandler<MoveQueueMessage> MoveQueueReceived;

        public event MessageReceivedEventHandler<SetShuffleMessage> SetShuffleReceived;

        public event MessageReceivedEventHandler<InsertSongsMessage> InsertSongReceived;

        public event MessageReceivedEventHandler<ReplaceQueueMessage> ReplaceQueueReceived;

        public event MessageReceivedEventHandler<AddSongsMessage> AddSongsReceived;

        public event MessageReceivedEventHandler<ClearQueueMessage> ClearQueueReceived;

        public event MessageReceivedEventHandler<ResetQueueMessage> ResetQueueReceived;

        public event MessageReceivedEventHandler<Messages.Client.SetCurrentSongMessage> SetCurrentSongReceived;

        public event MessageReceivedEventHandler<UpdatePlaylistMessage> PlaylistUpdatedReceived;

        public event RequestReceivedEventHandler<ListPlaylistsRequest, ListPlaylistResponse> ListPlaylistReceived;

        public event RequestReceivedEventHandler<PlaylistDetailsRequest, PlaylistDetailsResponse> PlaylistDetailsReceived;
    }

    public class Receiver : IReceiver
    {
        private bool disposed;
        private readonly IMessageQueue messageQueue;

        public Receiver(IMessageQueue messageQueue)
        {
            this.messageQueue = messageQueue;

            messageQueue.AddMessageConsumer<SuggestionMessage>((c, m) => InvokeMessageHandler(c, m, SuggestionReceived));
            messageQueue.AddMessageConsumer<MoveQueueMessage>((c, m) => InvokeMessageHandler(c, m, MoveQueueReceived));
            messageQueue.AddMessageConsumer<SetShuffleMessage>((c, m) => InvokeMessageHandler(c, m, SetShuffleReceived));
            messageQueue.AddMessageConsumer<InsertSongsMessage>((c, m) => InvokeMessageHandler(c, m, InsertSongReceived));
            messageQueue.AddMessageConsumer<ReplaceQueueMessage>((c, m) => InvokeMessageHandler(c, m, ReplaceQueueReceived));
            messageQueue.AddMessageConsumer<AddSongsMessage>((c, m) => InvokeMessageHandler(c, m, AddSongsReceived));
            messageQueue.AddMessageConsumer<ClearQueueMessage>((c, m) => InvokeMessageHandler(c, m, ClearQueueReceived));
            messageQueue.AddMessageConsumer<ResetQueueMessage>((c, m) => InvokeMessageHandler(c, m, ResetQueueReceived));
            messageQueue.AddMessageConsumer<Messages.Client.SetCurrentSongMessage>((c, m) => InvokeMessageHandler(c, m, SetCurrentSongReceived));
            messageQueue.AddMessageConsumer<UpdatePlaylistMessage>((c, m) => InvokeMessageHandler(c, m, PlaylistUpdatedReceived));

            messageQueue.AddRequestConsumer<ListPlaylistsRequest, ListPlaylistResponse>((c, m) => InvokeRequestHandler(c, m, ListPlaylistReceived));
            messageQueue.AddRequestConsumer<PlaylistDetailsRequest, PlaylistDetailsResponse>((c, m) => InvokeRequestHandler(c, m, PlaylistDetailsReceived));
        }

        public event MessageReceivedEventHandler<SuggestionMessage> SuggestionReceived;
        public event MessageReceivedEventHandler<MoveQueueMessage> MoveQueueReceived;
        public event MessageReceivedEventHandler<SetShuffleMessage> SetShuffleReceived;
        public event MessageReceivedEventHandler<InsertSongsMessage> InsertSongReceived;
        public event MessageReceivedEventHandler<ReplaceQueueMessage> ReplaceQueueReceived;
        public event MessageReceivedEventHandler<AddSongsMessage> AddSongsReceived;
        public event MessageReceivedEventHandler<ClearQueueMessage> ClearQueueReceived;
        public event MessageReceivedEventHandler<ResetQueueMessage> ResetQueueReceived;
        public event MessageReceivedEventHandler<Messages.Client.SetCurrentSongMessage> SetCurrentSongReceived;
        public event MessageReceivedEventHandler<UpdatePlaylistMessage> PlaylistUpdatedReceived;
        
        public event RequestReceivedEventHandler<ListPlaylistsRequest, ListPlaylistResponse> ListPlaylistReceived;
        public event RequestReceivedEventHandler<PlaylistDetailsRequest, PlaylistDetailsResponse> PlaylistDetailsReceived;

        public void Dispose()
        {
            if (!disposed)
            {
                messageQueue.Dispose();
                disposed = true;
            }

            GC.SuppressFinalize(this);
        }

        private static Response InvokeMessageHandler<T>(Client client, T message, MessageReceivedEventHandler<T> handler)
        {
            MessageEventArgs<T> args = new(message);
            handler?.Invoke(client, args);
            return args.Response;
        }

        private static Response<TResponse> InvokeRequestHandler<TRequest, TResponse>(Client client, TRequest request, RequestReceivedEventHandler<TRequest, TResponse> handler)
        {
            RequestEventArgs<TRequest, TResponse> args = new(request);
            handler?.Invoke(client, args);
            return args.Response;
        }
    }
}
