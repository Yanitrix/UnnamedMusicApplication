using Domain.DataAccess;
using Domain.Queue;

namespace Domain
{
    public interface ISession
    {
        public ISongQueue Queue { get; }
        public IPlaylistRepository PlaylistRepository { get; set; }
    }
}