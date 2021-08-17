using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Queue
{
    public class ShufflePlayingMode : PlayingMode
    {
        private List<int> history;
        private int farthestIndex;
        private static readonly Random rng = new();
        
        public ShufflePlayingMode(int count, int current = -1) : base(count, current)
        {
            history = new List<int>(count);
            for (int i = 0; i < count; i++)
                history.Add(i);
            Shuffle();
        }

        public override bool HasNext => current < count - 1;

        public override int Next
        {
            get
            {
                var next = history[++current];
                if (next > farthestIndex)
                    farthestIndex = next;
                return next;
            }
        }

        public override int Count
        {
            get => count;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Cannot be lower than 0");
                
                if (value < current)
                    throw new InvalidOperationException("Not possible");

                if (value == count){}
                
                //optimization for +1 incrementation
                else if (value == count + 1)
                {
                    count = value;
                    var index = rng.Next(farthestIndex, count) + 1;
                    var tmp = new List<int>(count);
                    for (int i = 0; i < index; i++) 
                        tmp[i] = history[i];
                    for (int i = index + 1; i < count; i++)
                        tmp[i] = history[i - 1];
                    tmp[index] = value;

                    history = tmp;
                }

                else if (value > count)
                {
                    var tmp = new List<int>(count);
                    for (int i = 0; i < count; i++)
                        tmp[i] = history[i];
                    for (int i = count; i < value; i++)
                        tmp[i] = i;

                    history = tmp;
                    count = value;
                    Shuffle();
                }

                else if (value < count)
                {
                    //offset <- higher than value and lower than currentIndex
                    //offset <- higher than value and lower than farthestIndex
                    //move current and farthest by these values
                    var offset = history.Count(i => i > value && i <= current);
                    current -= offset;
                    offset = history.Count(i => i > value && i <= farthestIndex);
                    farthestIndex -= offset;
                    
                    //then remove those indices from history
                    history.RemoveAll(i => i > value);
                }
            }
        }

        public override bool HasPrevious => current >= 0;

        public override int Previous => history[--current];

        public override int Current => history[current];
        
        public override void Reset()
        {
            current = -1;
        }

        private void Shuffle()
        {
            var begin = farthestIndex + 1;
            int count = this.count - begin;
            
            for (int current = begin; current < (count - 1); current++)
            {
                int random = current + rng.Next(count);
                int temp = history[random];
                history[random] = history[current];
                history[current] = temp;
            }   
        }
    }
}