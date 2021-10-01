using System.Collections;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Queue;
using Networking;

namespace Infrastructure.Session
{
    public class NetworkQueueProxy : ISongQueue
    {
        private readonly ISongQueue queue;
        private readonly IMessenger messenger;

        public IEnumerator<Song> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Song Next { get; }
        public Song Previous { get; }
        public bool HasNext { get; }
        public bool HasPrevious { get; }
        public bool Shuffle { get; set; }
        
        public void Insert(Song song)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(IEnumerable<Song> songs)
        {
            throw new System.NotImplementedException();
        }

        public void Replace(IEnumerable<Song> songs)
        {
            throw new System.NotImplementedException();
        }

        public void Add(Song song)
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public void Set(IEnumerable<Song> songs)
        {
            throw new System.NotImplementedException();
        }

        public void Reset()
        {
            throw new System.NotImplementedException();
        }
    }
}