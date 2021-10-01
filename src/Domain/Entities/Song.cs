using System;

namespace Domain.Entities
{
    public class Song : BaseEntity
    {
        //combination of name and album unique

        public string Path { get; set; }

        //tag properties
        public int TrackNo { get; set; }
        public DateTime DateReleased { get; set; }
        public TimeSpan Duration { get; set; }
        public string Comments { get; set; }

        public long AlbumId { get; set; }
        public Album Album { get; set; }
        
        /// <summary>
        /// now only for unit testing
        /// </summary>
        /// <param name="obj"></param>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (this == obj) return true;

            Song that = obj as Song;
            if (that == null) return false;

            return this.Id == that.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Path, TrackNo, DateReleased, Duration, Comments, Album);
        }
    }
}
