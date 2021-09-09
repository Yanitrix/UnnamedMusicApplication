
using System;

namespace Domain.Queue
{
    public abstract class PlayingMode
    {
        protected int count;
        protected int current;
        
        protected PlayingMode(int count, int current = -1)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count), "Count cannot be lower or equal to zero.");

            if (current < -1)
                throw new ArgumentOutOfRangeException(nameof(current), "Current cannot be lower than -1.");
            
            if (current >= count)
                throw new ArgumentOutOfRangeException(nameof(current), "Current cannot be greater or equal to count.");

            //idk if this should be here or somewhere in implementations 
            //yeah i think is should be only specific for linear mode
            // if (count == current + 1)
            //     throw new InvalidOperationException("Sequence cannot be empty.");
            
            this.count = count;
            this.current = current;
        }

        /// <summary>
        /// Throws <see cref="InvalidOperationException"/> if no next index is available.
        /// </summary>
        public abstract int Next { get; }

        public abstract bool HasNext { get; }

        public abstract int Count { get; set; }
        
        public abstract bool HasPrevious { get; }

        /// <summary>
        /// Throws <see cref="InvalidOperationException"/> if no previous index is available.
        /// </summary>
        public abstract int Previous { get; }

        public abstract int Current { get; }

        public virtual void Reset() => current = -1;
    }
}