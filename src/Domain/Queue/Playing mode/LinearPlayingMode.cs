using Domain.Exceptions;

namespace Domain.Queue
{
    public class LinearPlayingMode : PlayingMode
    {
        private int currentIndex;

        public override void Initialize(int begin, int last)
        {
            this.BeginIndex = begin;
            this.LastIndex = last;
            currentIndex = begin;
        }

        public override bool HasNext()
        {
            return currentIndex < LastIndex;
        }

        public override int Next()
        {
            if (!HasNext())
                throw new NoItemAvailableException("No next index available. The end was reached.");
            return ++currentIndex;
        }
    }
}
