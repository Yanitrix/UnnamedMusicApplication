using System;
using System.Collections.Generic;
using System.Text;

namespace UnnamedMusicApplication.Tags
{
    public interface ITagProvider
    {
        void Set(String path);

        void Clear();

        Dictionary<string, string> Tags { get; }
    }
}
