using System;
using System.Collections.Generic;
using System.Text;

namespace UnnamedMusicApplication.DataModel
{
    public class Playlist : BaseEntity
    {
        public IList<SongDto> SongDtos { get; set; }
    }
}
