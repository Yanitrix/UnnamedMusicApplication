using System;
using System.Collections.Generic;

namespace Domain.Tags
{
    public interface IFileTagFormat
    {
        /// <summary>
        /// An ordered collection of pairs of string and byte. 
        /// String is the name of the tag, byte represents number of bytes used to store the tag
        /// Collection NEEDS to be ordered, in order to keep the order.
        /// </summary>
        
        List<KeyValuePair<String, byte>> TagOrder { get; set; }

        bool TagsOnEnd { get; set; }
    }
}