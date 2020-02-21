using System.Collections.Generic;

namespace UnnamedMusicApplication.DataModel
{
    public class Artist : BaseEntity
    {
        public string Name { get; set; }
        /// <summary>
        /// Every song that doesn't have an Album is on an album called "unnamed" //or zero maybe gonna change it
        /// </summary>
        public IList<Album> Albums { get; set; } = new List<Album>();
    }
}