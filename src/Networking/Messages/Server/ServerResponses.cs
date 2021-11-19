using Domain.Entities;
using System.Collections.Generic;

namespace Networking.Messages.Server
{
    public record ListPlaylistResponse(List<Playlist> Playlists);

    public record PlaylistDetailsResponse(Playlist Playlist);
}
