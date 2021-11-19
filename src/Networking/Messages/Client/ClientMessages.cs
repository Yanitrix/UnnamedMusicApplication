using System.Collections.Generic;
using Domain.Entities;

namespace Networking.Messages.Client
{
    public record SuggestionMessage(List<Song> Songs);

    //queue actions
    public record MoveQueueMessage(int Direction);

    public record SetShuffleMessage(bool Shuffle);

    public record InsertSongsMessage(List<Song> Songs);

    public record ReplaceQueueMessage(List<Song> Songs);

    public record AddSongsMessage(List<Song> Songs);

    public record ClearQueueMessage();

    public record ResetQueueMessage();

    public record SetCurrentSongMessage(int Current);

    //other
    public record UpdatePlaylistMessage(Playlist Playlist);
}