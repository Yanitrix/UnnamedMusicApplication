using Domain.DataAccess.Abstract;
using Domain.Entities;
using System.Collections.Generic;

namespace Domain.DataAccess
{
    //TODO playlist should be updated when the files are rediscovered
    public interface IPlaylistRepository : IRepository<Playlist>
    {
        public IEnumerable<Playlist> All();

        //and here full crud can be performed
        public void Update(Playlist entity);

        public void Delete(Playlist entity);
    }
}
