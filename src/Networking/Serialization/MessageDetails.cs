using System;

namespace Networking.Serialization
{
    public record MessageDetails(byte MessageCode, Type MessageType, bool RequiresResponse, Type ResponseType);
}