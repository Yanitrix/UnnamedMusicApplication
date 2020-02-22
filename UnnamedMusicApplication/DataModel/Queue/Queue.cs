using System;
using System.Collections.Generic;
using System.Text;

namespace UnnamedMusicApplication.DataModel.Queue
{
    public class Queue : IQueue<Song>
    {
        private int currentIndex = 0;
        private List<Song> songs = new List<Song>();

        public Song Current { get => songs[currentIndex]; }
        public Song Next
        {
            get
            {
                currentIndex++;
                return Current;
            }
        }
        public Song Previous
        {
            get
            {
                currentIndex--;
                return Current;
            }
        }

        public void Insert(Song obj)
        {
            songs.Insert(currentIndex++, obj);
        }

        public void Insert(IEnumerable<Song> objs)
        {
            songs.InsertRange(currentIndex++, objs);
        }

        public void Jump(IEnumerable<Song> objs)
        {
            songs.RemoveRange(currentIndex++, songs.Count - currentIndex - 1);
            songs.AddRange(objs); 
        }

        public void Add(Song obj)
        {
            songs.Add(obj);
        }

        public void Clear()
        {
            songs.Clear();
        }
    }
}
