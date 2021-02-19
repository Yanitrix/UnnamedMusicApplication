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
        private int currentIndex;
        private readonly Random rng = new Random();

        public override void Initialize(int begin, int last)
        {
            this.BeginIndex = begin;
            this.LastIndex = last;
            this.currentIndex = begin;
            this.alreadyPlayed = new();
            this.possibleIndices = Enumerable.Range(currentIndex, LastIndex - currentIndex + 1).ToList();
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
                throw new NoIndexAvailableException();

            //if currentIndex is the last, everything's allright and the next random should be returned. 
            if (alreadyPlayed[^1] == currentIndex)
            {
                currentIndex = NextRandom();
            }
            //if currentIndex was already played (but is not the last element), the next index should be the next int in alreadyPlayed after currentIndex
            if (alreadyPlayed.Contains(currentIndex))
            {
                var no = alreadyPlayed.IndexOf(currentIndex);
                currentIndex = alreadyPlayed[no + 1];
            }

            alreadyPlayed.Add(currentIndex);
            possibleIndices.Remove(currentIndex);
            return currentIndex;
        }

        public override bool HasPrevious()
        {
            //possible only when the first index of alreadyPlayed is not the currentIndex
            //no need to check if alreadyPlayed.Contains(currentIndex) because that is not possible
            ///may be -1 <see cref="currentIndex"/>
            return currentIndex != -1 && alreadyPlayed[0] != currentIndex;
        }

        public override int Previous()
        {
            if (!HasPrevious())
                throw new NoIndexAvailableException();

            //current index was already played, so it should return the index previous to current one.
            var no = alreadyPlayed.IndexOf(currentIndex);
            currentIndex = alreadyPlayed[no - 1];
            return currentIndex;
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
