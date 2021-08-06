using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Queue
{
    public class ShuffleQueue : ISongQueue
    {
        private readonly List<int> history;
        private List<Song> songs;
        private ShuffledCollection possibleIndices;
        private int currentHistoryIndex; //history index
        private int head => history[^1];

        public ShuffleQueue(List<Song> songs, int currentIndex)
        {
            this.songs = songs;
            //history will be everything before and equal to current
            history = Enumerable.Range(0, currentIndex + 1).ToList();
            //possible - everything after current up to the songs.Length
            possibleIndices = new(songs.Count, currentIndex);
        }

        public Song Next
        {
            get
            {
                if (currentHistoryIndex != history.Count - 1)
                    return songs[history[++currentHistoryIndex]];
                
                var next = possibleIndices.Next;
                history.Add(next);
                return songs[next];
            }
        }

        public Song Previous => songs[history[--currentHistoryIndex]];

        public bool HasNext => possibleIndices.Count > 0;

        public bool HasPrevious => currentHistoryIndex > 0;

        public bool Shuffle { get => true; set { } }

        public void Add(Song song)
        {
            songs.Add(song);
            possibleIndices.Add(songs.Count - 1);
        }

        public void Clear()
        {
            songs.Clear();
            possibleIndices.Clear();
            history.Clear();
        }

        public void Insert(Song song)
        {
            var index = head;
            songs.Insert(index, song);
            possibleIndices.Add(index);
        }

        public void Insert(IEnumerable<Song> songs)
        {
            var index = head;
            this.songs.InsertRange(index, songs);
            var indices = Enumerable.Range(index, this.songs.Count - index + 1);
            possibleIndices.Add(indices);
        }

        public void Replace(IEnumerable<Song> songs)
        {
            var index = head;
            //remove all indices above current from history and possible
            history.RemoveAll(i => i > index);
            possibleIndices.Remove(i => i > index);
            //add to possible
            var indices = Enumerable.Range(index, songs.Count());
            possibleIndices.Add(indices);
        }

        public void Reset()
        {
            //keep history as it is
            //move index to beginning
            currentHistoryIndex = -1;
        }

        public void Set(IEnumerable<Song> songs)
        {
            if (this.songs.Count > 0)
                Clear();
            var list = songs.ToList();
            this.songs = list;
            //possible - everything after current up to the songs.Length
            possibleIndices = new(list.Count);
        }
    }
}
