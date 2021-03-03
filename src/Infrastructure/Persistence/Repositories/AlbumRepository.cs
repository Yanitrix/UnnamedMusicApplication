using Domain.DataAccess;
using Domain.Entities;
using LiteDB;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Repositories
{
    public class AlbumRepository : BaseRepository<Album>, IAlbumRepository
    {
        public AlbumRepository(LiteDatabase connection) : base(connection)
        {
        }

        public override Album GetById(long id)
        {
            return repo.Include(BsonExpression.Create("$.Songs[*]")).FindById(id);
        }

        public override IEnumerable<Album> GetByName(string name)
        {
            return repo.Include(BsonExpression.Create("$.Songs[*]")).Find(Query.Contains("Name", name));
        }
    }
}
