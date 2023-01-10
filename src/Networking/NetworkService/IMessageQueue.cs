using Networking.Messages;
using Networking.Serialization;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Networking.NetworkService
{
    public interface IMessageQueue : IDisposable
    {
        void AddMessageConsumer<TMessage>(Func<Client, TMessage, Response> handler);

        void AddRequestConsumer<TMessage, TResponse>(Func<Client, TMessage, Response<TResponse>> handler);
    }

    public class MessageConsumer : IMessageQueue
    {
        private bool disposed;
        private const string UNAUHTORIZED_MESSAGE = "This type of request is not allowed for this client";

        private readonly IServer server;
        private readonly INetworkMessageSerializer serializer;
        private readonly IMessageDetailsProvider codes;

        private readonly IDictionary<Type, Delegate> consumers = new Dictionary<Type, Delegate>();
        private readonly Type responseType = typeof(Response<>);

        public MessageConsumer(IServer server, INetworkMessageSerializer serializer, IMessageDetailsProvider codes)
        {
            this.server = server;
            this.serializer = serializer;
            this.codes = codes;

            server.RequestReceived += this.RequestBytesReceived;
        }

        public void Dispose()
        {
            if (!disposed)
            {
                server.Dispose();
                disposed = true;
            }

            GC.SuppressFinalize(this);
        }

        public void AddMessageConsumer<TMessage>(Func<Client, TMessage, Response> consumer)
        {
            consumers[typeof(TMessage)] = consumer;
        }

        public void AddRequestConsumer<TMessage, TResponse>(Func<Client, TMessage, Response<TResponse>> consumer)
        {
            consumers[typeof(TMessage)] = consumer;
        }

        private void RequestBytesReceived(Client client, RequestEventArgs eventArgs)
        {
            var typeDetails = codes[eventArgs.RequestData[0]];

            object response;
            var message = DeserializeBytes(eventArgs.RequestData, typeDetails.MessageType);

            var authorized = CheckPermissions(client, typeDetails, out response);
            if (authorized)
            {
                response = consumers[typeDetails.MessageType].DynamicInvoke(client, message);
            }

            eventArgs.ResponseData = serializer.Serialize(response);
        }

        private object DeserializeBytes(byte[] bytes, Type messageType)
        {
            var messageBytes = new ArraySegment<byte>(bytes, 1, bytes.Length - 1);
            return serializer.Deserialize(messageType, messageBytes.ToArray()); //is that the whole array or just the cut one?
        }

        private bool CheckPermissions(Client client, MessageDetails messageDetails, out object response)
        {
            if (client.Permissions.IsAllowed(messageDetails.MessageType))
            {
                response = default;
                return true;
            }

            if (messageDetails.RequiresResponse)
            {
                response = UnauthorizeResponse(messageDetails.MessageType);
            }
            else
            {
                response = Response.Fail(UNAUHTORIZED_MESSAGE);
            }

            return false;
        }

        private object UnauthorizeResponse(Type typeOfResponse)
        {
            responseType.MakeGenericType(typeOfResponse);
            var method = responseType.GetMethod("Fail", BindingFlags.Public | BindingFlags.Static);
            var response = method.Invoke(responseType, new object[] { UNAUHTORIZED_MESSAGE });
            return response;
        }
    }
}
