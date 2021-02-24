using System.Collections.Generic;

namespace Domain.Entities
{
    public class Artist : BaseEntity
    {
        //name property should be unique, maybe even PK?

        /// <summary>
        /// Every song that doesn't have an Album is on an album called "unnamed" //or zero maybe gonna change it
        /// </summary>
        public IList<Album> Albums { get; } = new List<Album>();
    }
}