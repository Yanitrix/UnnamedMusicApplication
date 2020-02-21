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

        void Insert(T obj);

        void Insert(IEnumerable<T> objs);

        void Jump(IEnumerable<T> objs);

        void Add(T obj);

    }
}
