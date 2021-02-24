using System;
using System.Collections.Generic;

namespace Infrastructure.Tags
{
    public interface ITagProvider
    {
        IReadOnlyDictionary<String, String> ReadTags(String filePath);
    }
}
