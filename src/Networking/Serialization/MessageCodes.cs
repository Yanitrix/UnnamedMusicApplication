using System;
using Networking.Messages.Client;

namespace Networking.Serialization
{
    public static class MessageCodes
    {
        public static byte GetCode(Type messageType)
        {
            if (messageType == typeof(SuggestionMessage))
            {
                return 0;
            }
            else if (messageType == typeof(ReplaceQueueMessage))
            {
                return 1;
            }
            else
            {
                return 255;
            }
        }

        public static byte GetCode<T>() => GetCode(typeof(T));
    }
}