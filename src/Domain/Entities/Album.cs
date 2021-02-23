using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Album : Playlist
    {
        public DateTime DateReleased { get; set; }
        public IList<Artist> Artists { get; } = new List<Artist>();
    }
}