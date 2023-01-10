using Domain.DataAccess;
using Domain.MusicPlayer;
using Domain.Queue;

namespace Domain.Session
{
    public interface ISession
    {
        public ISongQueue Queue { get; }
        public IDatabase Database { get; }
        public IMusicPlayer Player { get; }
    }
}