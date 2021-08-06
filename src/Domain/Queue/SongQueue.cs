using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Queue
{
    public class SongQueue : ISongQueue
    {
        private readonly List<int> history; //the history can be used as a singleton list
        private readonly List<Song> songs;
        private int currentIndex = -1;
        
        private ISongQueue inner;

        public SongQueue()
        {
            inner = new LinearQueue(songs, currentIndex);
        }

        private bool shuffle;

        public Song Next => inner.Next;

        public Song Previous => inner.Previous;

        public bool HasNext => inner.HasNext;

        public bool HasPrevious => inner.HasPrevious;

        public bool Shuffle
        {
            get => shuffle;

            set
            {
                if (value == shuffle)
                    return;
                if (value)
                {
                    inner = new ShuffleQueue(this.songs, currentIndex);
                    shuffle = true;
                }
                else
                {
                    inner = new LinearQueue(this.songs, currentIndex);
                    shuffle = false;
                }
            }
        }

        public void Add(Song song) => inner.Add(song);

        public void Clear() => inner.Clear();

        public void Insert(Song song) => inner.Insert(song);

        public void Insert(IEnumerable<Song> songs) => inner.Insert(songs);

        public void Replace(IEnumerable<Song> songs) => inner.Replace(songs);

        public void Reset() => inner.Reset();

        public void Set(IEnumerable<Song> songs) => inner.Set(songs);
    }
}