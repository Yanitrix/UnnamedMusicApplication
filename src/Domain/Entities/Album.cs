using System;
using System.Collections.Generic;

namespace Domain.DataModel
{
    public class Album : Playlist
    {
        public DateTime DateReleased { get; set; }
        public IList<Artist> Artists { get; set; } = new List<Artist>();
    }
}