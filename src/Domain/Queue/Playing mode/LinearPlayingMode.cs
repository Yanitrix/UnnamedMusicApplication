using Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace Domain.Queue
{
    public class LinearPlayingMode : PlayingMode
    {
        private int currentIndex = -1;

        public LinearPlayingMode(int count) : base(count) { }

        public LinearPlayingMode(int count, List<int> ignored):base(count, ignored) { }

        public override bool HasNext() => currentIndex < count - 1;

        public override int Next()
        {
            if (!HasNext())
                throw new NoItemAvailableException("No next index available. The end was reached.");
            return ++currentIndex;
        }

        /// <summary>
        /// Inclusive.
        /// </summary>
        public override void Insert(int firstIndex, int lastIndex)
        {
            if (firstIndex < lastIndex)
                throw new ArgumentException($"{nameof(firstIndex)} cannot be greater than {nameof(lastIndex)}");
            if (firstIndex < 0)
                throw new ArgumentException($"{nameof(firstIndex)} cannot be negative");

            var count = lastIndex - firstIndex + 1;
            this.count += count;

            if (lastIndex <= currentIndex)
                currentIndex += count;
        }

        /// <summary>
        /// Inclusive.
        /// </summary>
        public override void Remove(int firstIndex, int lastIndex)
        {
            if (firstIndex < lastIndex)
                throw new ArgumentException($"{nameof(firstIndex)} cannot be greater than {nameof(lastIndex)}");
            if (firstIndex < 0)
                throw new ArgumentException($"{nameof(firstIndex)} cannot be negative");
        }
    }
}
