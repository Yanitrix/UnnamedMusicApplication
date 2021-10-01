using System;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Queue
{
    public class SongQueue : ISongQueue
    {
        private readonly List<Song> content;
        private List<Song> history;
        private bool shuffle;

        public SongQueue()
        {
            content = new List<Song>();
            history = content;
        }
        
        public SongQueue(IEnumerable<Song> songs)
        {
            content = songs.ToList();
            history = content;
        }

        public IReadOnlyList<Song> Content => history.AsReadOnly();

        public int CurrentIndex { get; private set; } = -1;

        public Song Next => history[++CurrentIndex];

        public Song Previous => history[--CurrentIndex];

        public bool HasNext => CurrentIndex < history.Count - 1;

        public bool HasPrevious => CurrentIndex >= 0;

        public bool Shuffle
        {
            get => shuffle;
            set
            {
                if (value == shuffle)
                    return;
                if (value)
                {
                    //was linear before
                    history = content.ToList();
                    ShuffleHistory();
                    CurrentIndex = -1;
                    shuffle = true;
                }
                else
                {
                    //was shuffled before
                    var current = history[CurrentIndex];
                    history = content;
                    CurrentIndex = history.FindIndex(s => s.Id == current.Id);
                    shuffle = false;
                }
            }
        }

        public void Add(Song song)
        {
            ThrowIfShuffle();
            history.Add(song);
        }

        public void Clear()
        {
            ThrowIfShuffle();
            history.Clear();
        }

        public void Insert(Song song)
        {
            ThrowIfShuffle();
            history.Insert(CurrentIndex + 1, song);
        }

        public void Insert(IEnumerable<Song> songs)
        {
            ThrowIfShuffle();
            history.InsertRange(CurrentIndex + 1, songs);
        }

        public void Replace(IEnumerable<Song> songs)
        {
            ThrowIfShuffle();
            history.RemoveRange(CurrentIndex + 1, content.Count - CurrentIndex - 1);
            history.AddRange(songs.ToList());
        }

        public void Reset()
        {
            ThrowIfShuffle();
            CurrentIndex = -1;
        }

        public void Set(IEnumerable<Song> songs)
        {
            //should actually trow if shuffle?
            ThrowIfShuffle();
            content.RemoveRange(0, content.Count);
            content.AddRange(songs);
            history = content;
        }

        private void ThrowIfShuffle()
        {
            if (Shuffle)
                throw new InvalidOperationException("This method can be called only if the queue is not in shuffle mode.");
        }

        private void ShuffleHistory()
        {
            //either use currentIndex as the first of the shuffled list or place it somewhere in the middles
            //tbh, placing currentIndex as first seems more reasonable to me

            //place currentIndex somewhere by swapping
            //then take fragments of history and shuffle them accordingly
            if (CurrentIndex != -1)
                (history[CurrentIndex], history[0]) = (history[0], history[CurrentIndex]);
            
            Random rng = new();
            var count = history.Count;
            for (int current = 1; current < count - 1; current++)
            {
                int random = current + rng.Next(count - current);
                (history[random], history[current]) = (history[current], history[random]);
            }
        }
    }
}