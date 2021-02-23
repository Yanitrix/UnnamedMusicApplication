using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Queue
{
    public interface ISongQueue : IEnumerable<Song>
    {
        Song Next { get; }

        Song Previous { get; }

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
    }
}
