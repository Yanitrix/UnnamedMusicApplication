using System.Collections.Generic;

namespace Domain.Entities
{
    public class Playlist : BaseEntity
    {
        public virtual IList<Song> Songs { get; } = new List<Song>();
    }
}
