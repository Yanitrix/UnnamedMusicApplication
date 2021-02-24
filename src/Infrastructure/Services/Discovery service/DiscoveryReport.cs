using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class DiscoveryReport
    {
        public int ArtistCount { get; set; }

        public int AlbumCount { get; set; }

        public int SongCount { get; set; }

        public long TotalBytes { get; set; }
    }
}
