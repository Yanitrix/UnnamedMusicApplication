using Domain.DataAccess;
using Domain.Entities;
using LiteDB;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Repositories
{
    public class ArtistRepository : BaseRepository<Artist>, IArtistRepository
    {
        public ArtistRepository(LiteDatabase connection) : base(connection) { }

        //should i really return all entites? or maybe just names? if it's even possible
        public IEnumerable<Artist> All()
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
