using System.Collections.Generic;
using Domain.Entities;

namespace Networking.Messages.Client
{
    public record SuggestionMessage(List<Song> songs);

    public record ReplaceQueueMessage(List<Song> songs);
}