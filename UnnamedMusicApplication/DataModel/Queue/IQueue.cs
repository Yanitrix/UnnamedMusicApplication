using System;
using System.Collections.Generic;
using System.Text;

namespace UnnamedMusicApplication.DataModel.Queue
{
    interface IQueue<T>
    {
        T Current { get; }

        T Next { get; }

        T Previous { get; }


        /// <summary>
        /// Inserts an element after <code>Current</code> and keeps the rest of the queue the same.
        /// </summary>
        /// <param name="obj"></param>
        void Insert(T obj);

        /// <summary>
        /// Inserts a collection of elements after <code>Current</code> and keeps the rest of the queue the same.
        /// </summary>
        /// <param name="objs"></param>
        void Insert(IEnumerable<T> objs);

        /// <summary>
        /// Clears everything after <code>Current</code> and inserts a collection of elements.
        /// </summary>
        /// <param name="objs"></param>
        void Jump(IEnumerable<T> objs);

        /// <summary>
        /// Adds element to the end of the queue.
        /// </summary>
        /// <param name="obj"></param>
        void Add(T obj);

        /// <summary>
        /// Removes all elements from the queue.
        /// </summary>
        void Clear();

    }
}
