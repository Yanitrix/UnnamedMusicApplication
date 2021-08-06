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
        /// <param name="song"></param>
        void Insert(Song song);

        /// <summary>
        /// Inserts a collection of elements after current song and keeps the rest of the queue the same.
        /// </summary>
        /// <param name="songs"></param>
        void Insert(IEnumerable<Song> songs);

        /// <summary>
        /// Clears everything after current song and inserts a collection of elements.
        /// </summary>
        /// <param name="songs"></param>
        void Replace(IEnumerable<Song> songs);

        /// <summary>
        /// Adds element to the end of the queue.
        /// </summary>
        /// <param name="song"></param>
        void Add(Song song);

        /// <summary>
        /// Removes all elements from the queue.
        /// </summary>
        void Clear();

        /// <summary>
        /// Clears the whole queue and seeds it with given elements.
        /// </summary>
        /// <param name="songs"></param>
        void Set(IEnumerable<Song> songs);

        /// <summary>
        /// Sets the queue to a starting position. Keeps Shuffle value.
        /// So just basically start over again from the very beginning?
        /// </summary>
        void Reset();
    }
}
