using System;

namespace Domain.DataAccess
{
    public interface IDatabase : IDisposable
    {
        public IArtistRepository Artists { get; }

        public IAlbumRepository Albums { get; }

        public ISongRepository Songs { get; }

        public IPlaylistRepository Playlists { get; }

        public bool BeginTransaction();

        public bool CommitTransaction();

        /// <summary>
        /// Creates database. If db exists, no action is taken.
        /// </summary>
        public void Create();

        /// <summary>
        /// Removes all data from the database.
        /// </summary>
        public void Delete();
    }
}
