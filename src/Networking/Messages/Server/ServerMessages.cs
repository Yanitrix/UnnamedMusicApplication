using System.Collections.Generic;
using Domain.Entities;

namespace Networking.Messages.Server
{
    //send to client upon connecting or when closing connection with given client
    public record AuthorizationMessage(string Token, bool Accepted);

    //would be sent upon connecting
    public record QueueUpdatedMessage(List<Song> Songs, int Current, bool Shuffle);

    public record SetCurrentSongMessage(int Current);

    public record ShuffleChangedMessage(bool Shuffle);

    //Marks that Next / Previous was called on queue.
    public record QueueMovedMessage(int Direction);
}