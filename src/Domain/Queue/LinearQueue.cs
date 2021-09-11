using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Queue
{
    public class LinearQueue : ISongQueue
    {
        private readonly List<Song> songs;
        private int currentIndex;

        public LinearQueue(List<Song> songs, int currentIndex)
        {
            //we can just pregenerate the history lol
            this.songs = songs;
            this.currentIndex = currentIndex;
        }

        public Song Next => songs[++currentIndex];

        public Song Previous => songs[--currentIndex];

        public bool HasNext => currentIndex < songs.Count;

        public bool HasPrevious => currentIndex > 0;

        public bool Shuffle { get => false; set { } }

        public void Add(Song song)
        {
            songs.Add(song);
        }

        public void Clear()
        {
            currentIndex = -1;
            songs.Clear();
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
            this.songs.RemoveRange(currentIndex + 1, this.songs.Count - currentIndex);
        }

        public void Reset()
        {
            currentIndex = -1;
        }

        public void Set(IEnumerable<Song> songs)
        {
            Clear();
            this.songs.AddRange(songs);
        }
    }
}
