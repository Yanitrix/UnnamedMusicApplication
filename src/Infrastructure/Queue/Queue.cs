using Domain.DataModel;
using Domain.DataModel.Queue;
using Domain.Exceptions;
using Domain.Queue;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.SongQueue
{
    public class Queue : ISongQueue
    {
        //TODO should the indices in already played be moved over, when something is inserted?
        //because if some songs are inserted, then it may be, that the new indices of the song in current playlist were already played, and won't be again

        // should be -1 and then the first song is gonna be "Next"
        private int currentIndex = -1;

        //the list of indices of songs that were played, in order.
        //each index is the index of a song in this.songs
        private List<int> playedIndices = new();

        //all the songs that are in the playlist
        private readonly List<Song> songs;

        private PlayingMode mode;

        public Queue(List<Song> songs, PlayingMode playingMode)
        {
            this.songs = songs;
            PlayingMode = playingMode;
        }

        public Queue(List<Song> songs) : this(songs, PlayingModes.Normal) { }

        public PlayingMode PlayingMode
        {
            get => mode;
            set
            {
                mode = value ?? throw new ArgumentNullException("PlayingMode", "The PlayingMode cannot be null");
                mode.Initialize(currentIndex, songs.Count-1);
            }
        }

        public bool HasNext => mode.HasNext();

        public bool HasPrevious => !(playedIndices[0] == currentIndex || currentIndex == -1);

        public Song Next
        {
            get
            {
                if (!HasNext)
                    throw new NoItemAvailableException("There is no next song to be returned");
                StepForward();
                return songs[currentIndex];
            }
        }

        public Song Previous
        {
            get
            {
                if (!HasPrevious)
                    throw new NoItemAvailableException("There is no previous song to be returned");
                StepBackward();
                return songs[currentIndex];
            }
        }

        private void StepForward()
        {
            //if the current index is the head of this.playedIndices, then the next can be returned according to the mode
            if (playedIndices.Count == 0 || playedIndices[^1] == currentIndex)
            {
                currentIndex = mode.Next();
                playedIndices.Add(currentIndex);
            }

            //if it's not we need to find the next index in playedIndices
            else
            {
                var no = playedIndices.IndexOf(currentIndex);
                currentIndex = playedIndices[no + 1];

            }
        }

        private void StepBackward()
        {
            //the index before the current index in this.playedIndices
            var no = playedIndices.IndexOf(currentIndex);
            currentIndex = playedIndices[no - 1];
        }

        private void MoveLastIndexAndPlayedIndices(int offset)
        {
            mode.LastIndex += offset;
            //every index that is above currentIndex should be moved by the offset
            playedIndices = playedIndices.ConvertAll(i =>
            {
                if (i > currentIndex)
                    return i += offset;
                return i;
            });
        }

        //TODO what happens when mode.LastIndex is lower than it was? shouldn't indices be moved?
        public void Insert(Song song)
        {
            //mode.LastIndex moved by one
            //and every index after currentIndex moved by one
            MoveLastIndexAndPlayedIndices(1);
            songs.Insert(currentIndex + 1, song);
        }

        //all indexes above currentIndex will move by songs.Count
        public void Insert(IEnumerable<Song> songs)
        {
            var diff = songs.Count();
            //mode.LastIndex has to be moved 
            //and every song in playedIndices that is above currentIndex has to be moved
            MoveLastIndexAndPlayedIndices(diff);

            this.songs.InsertRange(currentIndex + 1, songs);
        }

        public void Replace(IEnumerable<Song> songs)
        {
            var replacementLength = songs.Count();
            var removedLength = mode.LastIndex - currentIndex;
            var diff = replacementLength - removedLength;
            //now, mode.LastIndex has to be moved by the difference
            //and every song in playedIndices that is above currentIndex has to be moved
            MoveLastIndexAndPlayedIndices(diff);

            this.songs.RemoveRange(currentIndex + 1, this.songs.Count - currentIndex - 1);
            this.songs.AddRange(songs);
        }

        public void Add(Song song)
        {
            songs.Add(song);
            mode.LastIndex++;
        }

        public void Clear()
        {
            songs.Clear();
            playedIndices.Clear();
            currentIndex = -1;
        }

        public void Set(IEnumerable<Song> songs)
        {
            Clear();
            Insert(songs);
            mode.LastIndex = songs.Count() - 1;
        }

        public IEnumerator<Song> GetEnumerator() => songs.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
