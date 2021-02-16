using Domain.DataModel;
using Domain.DataModel.Queue;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Queue
{
    public class Queue : IQueue<Song>
    {
        /// <summary>
        /// should be -1 and then the first song is gonna be "Next"
        /// </summary>
        /// 

        //TODO: think what happens when there's no Previous or Next
        private int currentIndex = -1;

        private readonly List<Song> songs = new List<Song>();

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
            songs.Insert(currentIndex + 1, obj);
        }

        public void Insert(IEnumerable<Song> objs)
        {
            songs.InsertRange(currentIndex + 1, objs);
        }

        public void Jump(IEnumerable<Song> objs)
        {
            songs.RemoveRange(currentIndex + 1, songs.Count - currentIndex - 1);
            songs.AddRange(objs);
        }

        public void Add(Song obj)
        {
            songs.Add(obj);
        }

        public void Clear()
        {
            songs.Clear();
            currentIndex = -1;
        }

        public void Set(IEnumerable<Song> objs)
        {
            Clear();
            Insert(objs);
        }

        public Song[] ToArray()
        {
            return songs.ToArray();
        }

    }
}
