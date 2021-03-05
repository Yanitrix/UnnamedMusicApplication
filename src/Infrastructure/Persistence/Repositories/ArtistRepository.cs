using Domain.DataAccess;
using Domain.Entities;
using LiteDB;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Persistence.Repositories
{
    public class ArtistRepository : BaseRepository<Artist>, IArtistRepository
    {
        public ArtistRepository(LiteDatabase connection) : base(connection) { }

        public override void Add(Artist entity)
        {
            var songs = entity.Albums.SelectMany(a => a.Songs).Where(s => s.ID == default);
            if (songs.Any())
                connection.GetCollection<Song>().InsertBulk(songs);
            var albums = entity.Albums.Where(a => a.ID == default);
            if (albums.Any())
                connection.GetCollection<Album>().InsertBulk(albums);
            repo.Insert(entity);

        }

        public override void Add(IEnumerable<Artist> entities)
        {
            var songs = entities.SelectMany(a => a.Albums).SelectMany(a => a.Songs).Where(s => s.ID == default);
            if (songs.Any())
                connection.GetCollection<Song>().InsertBulk(songs);
            var albums = entities.SelectMany(a => a.Albums).Where(a => a.ID == default);
            if (albums.Any())
                connection.GetCollection<Album>().InsertBulk(albums);
            repo.InsertBulk(entities);
        }

        //should i really return all entites? or maybe just names? if it's even possible
        public override IEnumerable<Artist> All()
        {
            return repo
                .Include(BsonExpression.Create("$.Albums[*]"))
                .Include(BsonExpression.Create("$.Albums[*].Songs[*]"))
                .FindAll();
        }

        public override Artist GetById(long id)
        {
            return repo
                .Include(BsonExpression.Create("$.Albums[*]"))
                .Include(BsonExpression.Create("$.Albums[*].Songs[*]"))
                .FindById(id);
        }

        public override IEnumerable<Artist> GetByName(string name)
        {
            return repo
               .Include(BsonExpression.Create("$.Albums[*]"))
               .Include(BsonExpression.Create("$.Albums[*].Songs[*]"))
               .Find(Query.Contains("Name", name));
        }
    }
}
