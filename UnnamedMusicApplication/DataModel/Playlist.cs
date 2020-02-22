using System;
using System.Collections.Generic;
using System.Text;

namespace UnnamedMusicApplication.DataModel
{
    public class Playlist : BaseEntity
    {
        public virtual IList<Song> Songs { get; set; } = new List<Song>();
    }
}
