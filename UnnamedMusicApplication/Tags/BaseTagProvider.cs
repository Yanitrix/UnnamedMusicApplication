using System;
using System.Collections.Generic;
using System.Text;

namespace UnnamedMusicApplication.Tags
{
    public abstract class BaseTagProvider : ITagProvider
    {

        //todo rethink if it would be better to use dictionary of names and values or fields with values
        public abstract int? TrackNo { get; set; }
        public abstract DateTime DateReleased { get; set; }
        public abstract string Comments { get; set; }
        public abstract string Album { get; set; }
        public abstract string Artist { get; set; }

        private IFileTagFormat tagFormat;

        //public abstract Dictionary MyProperty { get; set; }

        protected BaseTagProvider(IFileTagFormat format)
        {
            this.tagFormat = format;
        }

        public abstract void Clear();

        public abstract void Set(string path);

        //sets up the tags 
        protected abstract void SetUp();
        
    }
}
