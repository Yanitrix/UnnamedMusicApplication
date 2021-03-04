using System.Collections.Generic;

namespace Domain.Entities
{
    public class Playlist : BaseEntity
    {
        public virtual IList<Song> Songs { get; set; } = new List<Song>();
    }
}
