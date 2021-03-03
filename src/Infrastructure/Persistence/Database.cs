using Domain.DataAccess;
using Domain.Entities;
using Infrastructure.Persistence.Repositories;
using LiteDB;
using System;
using System.IO;

namespace Infrastructure.Persistence
{
    public class Database : IDatabase
    {
        private const string DATABASE_NAME = "songs";

        private readonly LiteDatabase connection;
        private bool disposed;

        private IArtistRepository artists;
        private IAlbumRepository albums;
        private ISongRepository songs;
        private IPlaylistRepository playlists;

        public Database()
        {
            this.connection = new LiteDatabase(GetDatabaseFilePath());
            Configure();
        }

        public IArtistRepository Artists => artists ??= new ArtistRepository(connection);

        public IAlbumRepository Albums => albums ??= new AlbumRepository(connection);

        public ISongRepository Songs => songs ??= new SongRepository(connection);

        public IPlaylistRepository Playlists => playlists ??= new PlaylistRepository(connection);

        public void Create()
        {
            var filePath = GetDatabaseFilePath();
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }
        }

        public void Delete()
        {
            var filePath = GetDatabaseFilePath();
            if (File.Exists(filePath))
            {
                if (!disposed)
                {
                    connection.DropCollection(typeof(Artist).Name);
                    connection.DropCollection(typeof(Album).Name);
                    connection.DropCollection(typeof(Song).Name);
                    connection.DropCollection(typeof(Playlist).Name);
                    Dispose();
                }
                File.Delete(filePath);
            }
        }

        public void Dispose()
        {
            if (!disposed)
            {
                connection.Dispose();
                GC.SuppressFinalize(this);
            }
            disposed = true;
        }

        private string GetDatabaseFilePath()
        {
            return $"{Directory.GetCurrentDirectory()}\\{DATABASE_NAME}.db";
        }

        private void Configure()
        {
            var mapper = BsonMapper.Global;

            mapper.Entity<Artist>()
                .DbRef(a => a.Albums)
                .Id(a => a.ID);

            mapper.Entity<Album>()
                .DbRef(a => a.Songs)
                .Id(a => a.ID);

            mapper.Entity<Playlist>()
                .DbRef(p => p.Songs)
                .Id(p => p.Name);
        }
    }
}
