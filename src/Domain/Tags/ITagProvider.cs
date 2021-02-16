using System;
using System.Collections.Generic;

namespace Domain.Tags
{
    public interface ITagProvider
    {
        IReadOnlyDictionary<String, String> ReadTags(String filePath);
    }
}
