using System.Collections.Generic;

namespace Domain.DataModel.Queue
{
    public interface ISongQueue : IEnumerable<Song>
    {
        Song Current { get; }

        Song Next { get; }

        Song Previous { get; }


        /// <summary>
        /// Inserts an element after <c>Current</c> and keeps the rest of the queue the same.
        /// </summary>
        /// <param name="obj"></param>
        void Insert(Song obj);

        /// <summary>
        /// Inserts a collection of elements after <c>Current</c> and keeps the rest of the queue the same.
        /// </summary>
        /// <param name="songs"></param>
        void Insert(IEnumerable<Song> songs);

        /// <summary>
        /// Clears everything after <c>Current</c> and inserts a collection of elements.
        /// </summary>
        /// <param name="songs"></param>
        void Replace(IEnumerable<Song> songs);

        /// <summary>
        /// Adds element to the end of the queue.
        /// </summary>
        /// <param name="obj"></param>
        void Add(Song obj);

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
