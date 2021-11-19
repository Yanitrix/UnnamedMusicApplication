namespace Networking.Messages.Client
{
    /// <summary>
    /// Response
    /// <seealso cref="Networking.Messages.Server.ListPlaylistResponse"/>
    /// </summary>
    public record ListPlaylistsRequest();

    /// <summary>
    /// Response
    /// <seealso cref="Networking.Messages.Server.PlaylistDetailsResponse"/>
    /// </summary>
    public record PlaylistDetailsRequest(string Name);
}
