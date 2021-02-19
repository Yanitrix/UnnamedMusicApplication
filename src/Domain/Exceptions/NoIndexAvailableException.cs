using System;

namespace Domain.Exceptions
{
    public class NoIndexAvailableException : Exception
    {
        public NoIndexAvailableException()
        {
        }

        public NoIndexAvailableException(string message) : base(message)
        {
        }

        public NoIndexAvailableException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
