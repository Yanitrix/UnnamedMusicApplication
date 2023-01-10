using System;
using System.Collections.Generic;

namespace Networking.Serialization
{
    //TODO maybe that could be read from some config?
    public interface IMessageDetailsProvider
    {
        MessageDetails this[byte messageCode] { get; }

        MessageDetails this[Type type] { get; }

        public IEnumerable<Type> MessageTypes { get; set; }

        public IEnumerable<Type> RequestTypes { get; set; }
    }
}