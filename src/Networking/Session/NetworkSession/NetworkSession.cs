using Domain;
using Domain.DataAccess;
using Domain.Queue;

namespace Infrastructure.Session.NetworkSession
{
    public class NetworkSession : ISession
    {
        public NetworkSession(NetworkQueueProxy queue, )
        {
            
        }
        
        public ISongQueue Queue { get; }
        public IDatabase Database { get; }
    }
}