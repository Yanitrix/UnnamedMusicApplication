using Domain.DataAccess;

namespace Networking.Session.NetworkSession
{
    public class NetworkDatabaseProxy : IDatabase
    {
        private readonly IDatabase database;
        

        public NetworkDatabaseProxy(IDatabase database)
        {
            this.database = database;
        }

        public IArtistRepository Artists { get; }
        public IAlbumRepository Albums { get; }
        public ISongRepository Songs { get; }
        public IPlaylistRepository Playlists { get; }
        public void Create()
        {
            throw new System.NotImplementedException();
        }

        public void Delete()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            database.Dispose();
        }
    }
}