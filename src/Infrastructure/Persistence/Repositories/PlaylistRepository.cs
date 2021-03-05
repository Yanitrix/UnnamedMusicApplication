using Domain.DataAccess;
using Domain.Entities;
using LiteDB;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Repositories
{
    public class PlaylistRepository : BaseRepository<Playlist>, IPlaylistRepository
    {
        public PlaylistRepository(LiteDatabase connection) : base(connection) { }

        public override IEnumerable<Playlist> All()
        {
            return repo
                .Include(BsonExpression.Create("$.Songs[*]"))
                .FindAll();
        }

        public void Delete(string name)
        {
            repo.Delete(name);
        }

        public void Update(Playlist entity)
        {
            repo.Update(entity);
        }
    }
}
