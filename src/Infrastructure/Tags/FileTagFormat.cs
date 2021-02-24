using System;
using System.Collections.Generic;

namespace Infrastructure.Tags
{
    public class FileTagFormat
    {
        /// <summary>
        /// An ordered collection of pairs of string and byte.
        /// String is the name of the tag, byte represents number of bytes used to store the tag
        /// Collection NEEDS to be ordered, in order to keep the order.
        /// </summary>

        //starts with dot
        public String FormatExtension { get; set; }

        public List<KeyValuePair<String, byte>> TagOrder { get; set; }

        public bool TagsOnEnd { get; set; }
    }
}