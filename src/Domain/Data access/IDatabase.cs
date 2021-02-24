
namespace Domain.DataAccess
{
    //TODO some mode to determine the uniqueness of entities? such as id (directory view only) vs names (determined upon song tags)
    //database should not be dependant on that ^, so i need to find a way to set it without calling this service
    public interface IDatabase
    {
        public IArtistRepository Artists { get; }

        public IAlbumRepository Albums { get; }

        public ISongRepository Songs { get; }

        public IPlaylistRepository Playlists { get; }

        public int SaveChanges();

        public void Create();

        public void Delete();
    }
}
