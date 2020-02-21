using System;
using System.Collections.Generic;
using System.Text;

namespace UnnamedMusicApplication.DataModel
{
    public class Song
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public int TrackNo { get; set; }
        public DateTime DateReleased { get; set; }
        public TimeSpan Duration { get; set; }
        public string Comments { get; set; }
        public Album Album { get; set; }

        //public Artist Artist { get; set; }
        // TODO: for now because can be got by album property, maybe will be added later 
    }
}
