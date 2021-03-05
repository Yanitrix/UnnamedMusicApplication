using Domain.Entities;
using LiteDB;
using System;
using System.IO;

namespace Infrastructure.Tests.Persistence
{
    public class DatabaseTestBase : IDisposable
    {
        protected LiteDatabase database;
        private string fileName, dbname;

        protected  DatabaseTestBase()
        {
            dbname = Guid.NewGuid().ToString();
            InitDb();
        }

        protected void InitDb()
        {
            fileName = $"{Directory.GetCurrentDirectory()}\\{dbname}.db";
            database = new LiteDatabase(fileName);

            var mapper = BsonMapper.Global;

            mapper
                .Entity<Artist>()
                .DbRef(a => a.Albums)
                .Id(a => a.Id);

            mapper
                .Entity<Album>()
                .DbRef(a => a.Songs)
                .Id(a => a.Id);

            mapper
                .Entity<Song>()
                .Id(s => s.Id);

            mapper
                .Entity<Playlist>()
                .DbRef(p => p.Songs)
                .Id(p => p.Name);
        }

        protected void Disconnect()
        {
            database.Dispose();
            InitDb();
        }

        public void Dispose()
        {
            database.Dispose();
            File.Delete(fileName);
            GC.SuppressFinalize(this);
        }
    }
}
