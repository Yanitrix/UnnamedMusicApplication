using System;
using System.Collections.Generic;

namespace UnnamedMusicApplication.DataModel
{
    public class Album
    {
        public string Name { get; set; }
        public DateTime DateReleased { get; set; }
        public IList<Song> Songs { get; set; } = new List<Song>();
        public IList<Artist> Artists { get; set; }

    }
}