using System;
using System.Collections.Generic;

namespace Domain.Queue
{
    /// <summary>
    /// Abstraction used to return a sequence of numbers (song indexes in a song queue) between given numbers.
    /// The sequence of the numbers returned is returned according to the implementation.
    /// </summary>
    public abstract class PlayingMode
    {
        protected int count;
        protected IList<int> ignored;

        protected PlayingMode(int count)
        {
            if (count <= 0)
                throw new ArgumentException("count cannot be lower or equal to zero", nameof(count));
            this.count = count;
        }

        protected PlayingMode(int count, IList<int> ignored) : this(count)
        {
            this.ignored = ignored;
        }

        /// <summary>
        /// Checks if there is anything left to return. If the end was reached, returns false.
        /// </summary>
        public abstract bool HasNext();

        /// <summary>
        /// This method should return the index of the song that is going to be played next.
        /// If the next index cannot be returned (e.g. the end was reached), <c>NoIndexAvailableException</c> is thrown.
        /// </summary>
        /// <returns>The index of the next song</returns>
        /// <exception cref="Domain.Exceptions.NoItemAvailableException">Thrown when there is no next index available.</exception>
        public abstract int Next();

        /// <summary>
        /// Inclusive
        /// </summary>
        public abstract void Insert(int firstIndex, int count);

        /// <summary>
        /// Inclusive
        /// </summary>
        public abstract void Remove(int firstIndex, int count);
    }
}
