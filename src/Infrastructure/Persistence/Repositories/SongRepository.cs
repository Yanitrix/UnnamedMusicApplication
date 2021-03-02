using Domain.DataAccess;
using Domain.Entities;
using LiteDB;

namespace Infrastructure.Persistence.Repositories
{
    public class SongRepository : BaseRepository<Song>, ISongRepository
    {
        public SongRepository(LiteDatabase connection, ISchemaProvider schemaProvider) : base(connection, schemaProvider) { }
    }
}
