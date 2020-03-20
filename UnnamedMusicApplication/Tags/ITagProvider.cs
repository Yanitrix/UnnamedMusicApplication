using System;
using System.Collections.Generic;
using System.Text;

namespace UnnamedMusicApplication
{
    public interface ITagProvider
    {
        void Set(String path);

        void Clear();

        int? TrackNo { get; set; }
        DateTime DateReleased { get; set; }
        string Comments { get; set; }
        string Album { get; set; }
        string Artist { get; set; }
    }
}
