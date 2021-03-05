using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Album : Playlist
    {
        //combination of artist and name should be unique, but how to do that?

        public DateTime DateReleased { get; set; }
        public IList<Artist> Artists { get; set; } = new List<Artist>();
    }
}