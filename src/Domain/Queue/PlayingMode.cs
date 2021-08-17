
namespace Domain.Queue
{
    public abstract class PlayingMode
    {
        protected int count;
        protected int current;
        
        protected PlayingMode(int count, int current = -1)
        {
            this.count = count;
            this.current = current;
        }

        public abstract int Next { get; }

        public abstract bool HasNext { get; }

        public abstract int Count { get; set; }
        
        public abstract bool HasPrevious { get; }

        public abstract int Previous { get; }

        public abstract int Current { get; }

        public virtual void Reset() => current = -1;
    }
}