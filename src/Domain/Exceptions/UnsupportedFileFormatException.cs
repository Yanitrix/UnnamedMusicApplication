using System;

namespace Domain.Exceptions
{
    public class UnsupportedFileFormatException : Exception
    {
        public UnsupportedFileFormatException()
        {
        }

        public UnsupportedFileFormatException(string message) : base(message)
        {
        }

        public UnsupportedFileFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
