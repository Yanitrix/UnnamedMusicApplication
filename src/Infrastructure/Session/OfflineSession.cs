using Domain;
using Domain.DataAccess;
using Domain.Queue;
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
        
        public ISongQueue Queue { get; }
        public IDatabase Database { get; }
    }
}