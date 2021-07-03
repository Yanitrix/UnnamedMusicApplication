using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Queue
{
    public class ShufflePlayingMode : PlayingMode
    {
        //has to keep order
        private List<int> alreadyPlayed;
        private List<int> possibleIndices;
        //may be -1, it indicates that the playing mode was used right from start of the playlist (that means that the random indices were in range [0, LastIndex])
        private readonly Random rng = new();

        protected ShufflePlayingMode(int count) : base(count)
        {
        }

        protected ShufflePlayingMode(int count, IList<int> ignored) : base(count, ignored)
        {
        }

        public override bool HasNext()
        {
            //check if there are any numbers between (currentIndex, LastIndex] that weren't played
            return possibleIndices.Count > 0;
        }

        public override int Next()
        {
            //the next index is gonna be within (currentIndex, LastIndex], excluding already played
            if (!HasNext())
                throw new NoItemAvailableException("No next index available. The end was reached.");

            //if currentIndex is the last, everything's allright and the next random should be returned. 
            var next = NextRandom();

            alreadyPlayed.Add(next);
            possibleIndices.Remove(next);
            return next;
        }

        private int NextRandom()
        {
            //random within range (currentIndex, LastIndex] with exception of this.alreadyPlayed
            //so that means just get a random from this.possible
            var index = rng.Next(0, possibleIndices.Count);
            return possibleIndices[index];
        }
    }
}
