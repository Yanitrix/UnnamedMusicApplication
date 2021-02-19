using System;

namespace Domain.Exceptions
{
    public class NoItemAvailableException : Exception
    {
        public NoItemAvailableException()
        {
        }

        public NoItemAvailableException(string message) : base(message)
        {
        }

        public NoItemAvailableException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
