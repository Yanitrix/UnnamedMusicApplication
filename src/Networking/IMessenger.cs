using Networking.Messages.Server;

namespace Networking
{
    public interface IMessenger
    {
        public void QueueUpdated(QueueUpdatedMessage msg);

        public void QueueMoved(QueueMovedMessage msg);
        
        
    }
}