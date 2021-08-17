using System;

namespace Domain.Queue
{
    public class LinearPlayingMode : PlayingMode
    {
        public LinearPlayingMode(int count, int current = -1) : base(count, current)
        {
        }

        public override int Next => ++current;

        public override bool HasNext => current < count - 1;
        
        public override int Count
        {
            get => count;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Cannot be lower than zero", nameof(Count));

                if (value - 1 < current)
                    current = value - 1;

                count = value;
            }
        }

        public override bool HasPrevious => current >= 0;

        public override int Previous => --current;

        public override int Current => current;
        
        public override void Reset()
        {
            current = -1;
        }
    }
}