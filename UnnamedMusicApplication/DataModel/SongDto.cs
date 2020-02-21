using System;
using System.Collections.Generic;
using System.Text;

namespace UnnamedMusicApplication.DataModel
{
    public class SongDto : BaseEntity
    {
        public Playlist Playlist { get; set; }

        public Song Instance { get; set; } //unique song for each playlist
    }
}
