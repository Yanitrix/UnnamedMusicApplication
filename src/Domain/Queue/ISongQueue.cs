using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Queue
{
    public interface ISongQueue
    {
        Song Next { get; }

        Song Previous { get; }

        public bool HasNext { get; }

        public bool HasPrevious { get; }

        public bool Shuffle { get; set; }

        /// <summary>
        /// Inserts an element after current song and keeps the rest of the queue the same.
        /// </summary>
        void Insert(Song song);

        /// <summary>
        /// Inserts a collection of elements after current song and keeps the rest of the queue the same.
        /// </summary>
        void Insert(IEnumerable<Song> songs);

        /// <summary>
        /// Clears everything after current song and inserts a collection of elements.
        /// </summary>
        void Replace(IEnumerable<Song> songs);

        //WHY no overload for adding multiple songs?
        /// <summary>
        /// Adds element at the end of the queue.
        /// </summary>
        void Add(Song song);

        /// <summary>
        /// Adds a collection of elements at the end of the queue.
        /// </summary>
        void Add(IEnumerable<Song> songs);

        /// <summary>
        /// Removes all elements from the queue.
        /// </summary>
        void Clear();

        /// <summary>
        /// Clears the whole queue and seeds it with given elements.
        /// </summary>
        void Set(IEnumerable<Song> songs);

        /// <summary>
        /// Sets the queue to a starting position. Keeps Shuffle value.
        /// So just basically start over again from the very beginning?
        /// </summary>
        void Reset();
        
        /// <summary>
        /// Returns all songs in the queue in the current order of playing.
        /// </summary>
        IReadOnlyList<Song> Content { get; }
        
        /// <summary>
        /// Returns index of current song.
        /// </summary>
        public int CurrentIndex { get; set; }
    }
}
