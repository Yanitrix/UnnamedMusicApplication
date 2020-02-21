using System;
using System.Collections.Generic;
using System.Text;

namespace UnnamedMusicApplication.DataModel.Queue
{
    public class Queue : IQueue<SongDto>
    {
        private int currentIndex = 0;
        private List<SongDto> songs = new List<SongDto>();

        public SongDto Current { get => songs[currentIndex]; }
        public SongDto Next
        {
            get
            {
                currentIndex++;
                return Current;
            }
        }
        public SongDto Previous
        {
            get
            {
                currentIndex--;
                return Current;
            }
        }

        public void Insert(SongDto obj)
        {
            songs.Insert(currentIndex++, obj);
        }

        public void Insert(IEnumerable<SongDto> objs)
        {
            songs.InsertRange(currentIndex++, objs);
        }

        public void Jump(IEnumerable<SongDto> objs)
        {
            songs.RemoveRange(currentIndex++, songs.Count - currentIndex - 1);
            songs.AddRange(objs); 
        }

        public void Add(SongDto obj)
        {
            songs.Add(obj);
        }

    }
}
