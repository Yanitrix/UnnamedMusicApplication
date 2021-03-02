using System;

namespace Domain.DataAccess
{
    public interface IDatabase : IDisposable
    {
        public IArtistRepository Artists { get; }

        public IAlbumRepository Albums { get; }

        public ISongRepository Songs { get; }

        public IPlaylistRepository Playlists { get; }

        /// <summary>
        /// Creates database. If db exists, no action is taken.
        /// </summary>
        public void Create();

        /// <summary>
        /// Removes database. If db does not exist, no action is taken.
        /// </summary>
        public void Delete();
    }
}
