using System;
using Domain.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Queue
{
    public class SongQueue : ISongQueue
    {
        private int currentIndex = -1;
        private readonly List<Song> songs = new();
        private readonly List<int> shuffledIndices = new();
        private bool shuffle;

        public Song Next => HasNext ? songs[NextIndex()] : null;

        public Song Previous => HasPrevious ? songs[PreviousIndex()] : null;

        public bool HasNext => shuffle ? currentIndex != shuffledIndices[^1] : currentIndex != songs.Count - 1;

        public bool HasPrevious => shuffle ? currentIndex != shuffledIndices[0] : currentIndex != 0;

        public bool Shuffle
        {
            get => shuffle;
            set
            {
                if (value && !shuffle)
                    StartShuffle();
                else
                    EndShuffle();
                shuffle = value;
            }
        }

        public void Insert(Song song)
        {
            songs.Insert(currentIndex + 1, song);
        }

        public void Insert(IEnumerable<Song> songs)
        {
            this.songs.InsertRange(currentIndex + 1, songs);
        }

        public void Replace(IEnumerable<Song> songs)
        {
            this.songs.RemoveRange(currentIndex + 1, this.songs.Count - currentIndex - 1);
            this.songs.AddRange(songs);
        }

        public void Add(Song song)
        {
            songs.Add(song);
        }

        public void Clear()
        {
            songs.Clear();
            currentIndex = -1;
        }

        public void Set(IEnumerable<Song> songs)
        {
            Clear();
            Insert(songs);
        }

        public void Reset()
        {
            currentIndex = -1;
            if (!shuffle) return;
            EndShuffle();
            StartShuffle();
        }

        public IEnumerator<Song> GetEnumerator() => songs.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void StartShuffle()
        {
            var range = Enumerable.Range(currentIndex + 1, songs.Count - currentIndex - 1);
            var rng = new Random();
            shuffledIndices.Add(currentIndex);
            shuffledIndices.AddRange(range.OrderBy(_ => rng.Next()).ToList());
        }

        private void EndShuffle()
        {
            shuffledIndices.Clear();
        }

        private int NextIndex()
        {
            if (shuffle)
            {
                var idx = shuffledIndices.IndexOf(currentIndex);
                currentIndex = shuffledIndices[idx + 1];
                return currentIndex;
            }

            return ++currentIndex;
        }

        private int PreviousIndex()
        {
            if (shuffle)
            {
                var idx = shuffledIndices.IndexOf(currentIndex);
                currentIndex = shuffledIndices[idx - 1];
                return currentIndex;
            }

            return --currentIndex;
        }
    }
}