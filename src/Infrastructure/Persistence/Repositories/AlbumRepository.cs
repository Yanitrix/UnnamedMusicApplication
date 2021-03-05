using Domain.DataAccess;
using Domain.Entities;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Persistence.Repositories
{
    public class AlbumRepository : BaseRepository<Album>, IAlbumRepository
    {
        public AlbumRepository(LiteDatabase connection) : base(connection)
        {
        }

        public override void Add(Album entity)
        {
            var songs = entity.Songs.Where(s => s.ID == default);
            if (songs.Any())
                connection.GetCollection<Song>().InsertBulk(songs);
            repo.Insert(entity);
        }

        public override void Add(IEnumerable<Album> entities)
        {
            var songs = entities.SelectMany(a => a.Songs).Where(s => s.ID == default);
            if (songs.Any())
                connection.GetCollection<Song>().InsertBulk(songs);
            repo.InsertBulk(entities);
        }

        public override IEnumerable<Album> All()
        {
            return repo.Include(x => x.Songs).FindAll();
        }

        public override Album GetById(long id)
        {
            return repo.Include(BsonExpression.Create("$.Songs[*]")).FindById(id);
        }

        public override IEnumerable<Album> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Array.Empty<Album>();
            return repo.Include(BsonExpression.Create("$.Songs[*]")).Find(Query.Contains("Name", name));
        }
    }
}
