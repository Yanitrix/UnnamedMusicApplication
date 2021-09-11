using System.Collections.Generic;
using System.Linq;
using Domain.Entities;

namespace Domain.Queue
{
    public class SongQueue : ISongQueue
    {
        private readonly List<Song> songs;
        private PlayingMode mode;
        
        public SongQueue(List<Song> songs)
        {
            this.songs = songs; 
        }

        public bool HasNext => mode.HasNext;
        
        public Song Next => songs[mode.Next];

        public bool HasPrevious => mode.HasPrevious;

        public Song Previous => songs[mode.Previous];

        public bool Shuffle { get; set; }

        public void Insert(Song song)
        {
            songs.Insert(mode.Current, song);
            mode.Count++;
        }

        public void Insert(IEnumerable<Song> songs)
        {
            var enumerable = songs as Song[] ?? songs.ToArray();
            var count = enumerable.Length + this.songs.Count;
            this.songs.AddRange(enumerable);
            mode.Count = count;
        }

        public void Replace(IEnumerable<Song> songs)
        {
            var count = (mode.Current, this.songs.Count - 1).CountBetween();
            
            this.songs.RemoveRange(mode.Current, count);
            var enumerable = songs as Song[] ?? songs.ToArray();
            var offset = enumerable.Length - count;
            mode.Count += offset;
            this.songs.AddRange(enumerable);
        }

        public void Add(Song song)
        {
            songs.Add(song);
            mode.Count++;
        }

        public void Clear()
        {
            songs.Clear();
        }

        public void Set(IEnumerable<Song> songs)
        {
            this.songs.Clear();
            var enumerable = songs.ToList();
            var count = enumerable.Count;
            this.songs.AddRange(enumerable);
        }

        public void Reset() => mode.Reset();
    }
}