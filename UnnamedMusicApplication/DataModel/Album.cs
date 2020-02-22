using System;
using System.Collections.Generic;

namespace UnnamedMusicApplication.DataModel
{
    public class Album : Playlist
    {
        public DateTime DateReleased { get; set; }
        public IList<Artist> Artists { get; set; }
    }
}