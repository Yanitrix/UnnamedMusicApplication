using System.Collections.Generic;
using Domain.Entities;

namespace Networking.Messages.Server
{
    public record QueueUpdatedMessage(List<Song> Songs, int Current);

    // Marks that Next / Previous was called on queue. 
    public record QueueMovedMessage(int Direction);

    public record ListPlaylistResponse(List<Playlist> Playlists);
}