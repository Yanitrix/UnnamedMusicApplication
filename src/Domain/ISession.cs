using Domain.DataAccess;
using Domain.Queue;

namespace Domain
{
    public interface ISession
    {
        public ISongQueue Queue { get; }
        public IDatabase Database { get; }
    }
}