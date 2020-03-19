using System;
using System.Collections.Generic;
using System.Text;

namespace UnnamedMusicApplication.DataModel
{
    public class Song : BaseEntity
    {
        public string Path { get; set; }
        public int TrackNo { get; set; }
        public DateTime DateReleased { get; set; }
        public TimeSpan Duration { get; set; }
        public string Comments { get; set; }
        public Album Album { get; set; }

        //public Artist Artist { get; set; }
        // TODO: for now because can be got by album property, maybe will be added later 


        /// <summary>
        /// now only for unit testing
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (this == obj) return true;

            Song that = obj as Song;
            if (that == null) return false;

            if (this.Name.Equals(that.Name) && this.ID == that.ID) return true;
            return false;
        }
    }
}
