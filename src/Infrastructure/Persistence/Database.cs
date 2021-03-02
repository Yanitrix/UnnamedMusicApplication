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
        private readonly LiteDatabase connection;
        private readonly ISchemaProvider provider;

        private IArtistRepository artists;
        private IAlbumRepository albums;
        private ISongRepository songs;
        private IPlaylistRepository playlists;

        public Database(ISchemaProvider provider)
        {
            this.connection = new LiteDatabase(GetDatabaseFilePath());
            this.provider = provider;

            Configure();
        }

        public IArtistRepository Artists => artists ??= new ArtistRepository(connection, provider);

        public IAlbumRepository Albums => albums ??= new AlbumRepository(connection, provider);

        public ISongRepository Songs => songs ??= new SongRepository(connection, provider);

        public IPlaylistRepository Playlists => playlists ??= new PlaylistRepository(connection, provider);

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
                connection.DropCollection(provider.ArtistCollectionName);
                connection.DropCollection(provider.AlbumCollectionName);
                connection.DropCollection(provider.SongCollectionName);
                connection.DropCollection(provider.PlaylistCollectionName);
                File.Delete(filePath);
            }
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

        protected string GetDatabaseFilePath()
        {
            return $"{Directory.GetCurrentDirectory()}\\{provider.DatabaseName}.sqlite";
        }

        public void Dispose()
        {
            connection.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
