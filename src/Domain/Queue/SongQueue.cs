using Domain.Entities;
using System.Collections;
using System.Collections.Generic;

namespace Domain.Queue
{
    public class SongQueue : ISongQueue
    {

        // should be -1 and then the first song is gonna be "Next"
        //TODO: think what happens when there's no Previous or Next
        private int currentIndex = -1;

        private readonly List<Song> songs = new List<Song>();

        public Song Next
        {
            get
            {
                currentIndex++;
                return songs[currentIndex];
            }
        }
        public Song Previous
        {
            get
            {
                currentIndex--;
                return songs[currentIndex];
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

        public IEnumerator<Song> GetEnumerator() => songs.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
