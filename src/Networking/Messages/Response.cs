using System.Collections.Generic;

namespace Networking.Messages
{
    public sealed class Response
    {
        public bool Success { get; }

        public IList<string> Messages { get; }

        private Response(bool success, IList<string> messages)
        {
            Success = success;
            Messages = messages;
        }

        public static Response Succeed() => new(true, new List<string>());

        public static Response Fail(string message) => new(false, new List<string> { message });

        public static Response Fail(IList<string> messages) => new(false, messages);
    }

    public sealed class Response<T>
    {
        public T Entity { get; }

        public bool Success { get; }

        public IList<string> Messages { get; }

        private Response(T entity, bool success, IList<string> messages)
        {
            Success = success;
            Entity = entity;
            Messages = messages;
        }

        public static Response<T> Succeed(T entity) => new(entity, true, new List<string>());

        public static Response<T> Fail(string message) => new(default, false, new List<string> { message });

        public static Response<T> Fail(IList<string> messages) => new(default, false, messages);

        public static implicit operator Response<T>(T message) => Succeed(message);

        public static implicit operator T(Response<T> response) => response.Entity;
    }
}
