using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Networking
{
    public class QueueChangedEventArgs : EventArgs
    {
        public IList<Song> Songs { get; set; }
        public int Current { get; set; }
        public bool Shuffle { get; set; }
    }
}