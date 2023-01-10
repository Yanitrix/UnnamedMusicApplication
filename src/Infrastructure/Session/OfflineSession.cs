using Domain.DataAccess;
using Domain.MusicPlayer;
using Domain.Queue;
using Domain.Session;
using Infrastructure.Persistence;

namespace Infrastructure.Session
{
    public class OfflineSession : ISession
    {
        public OfflineSession(SongQueue queue, Database database)
        {
            Queue = queue;
            Database = database;
        }
        
        public ISongQueue Queue => 
        public IDatabase Database { get; }

        public IMusicPlayer Player => ;
    }
}