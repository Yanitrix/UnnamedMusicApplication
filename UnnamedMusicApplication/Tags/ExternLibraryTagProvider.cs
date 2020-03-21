﻿using Id3;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace UnnamedMusicApplication.Tags
{
    public class ExternLibraryTagProvider : ITagProvider
    {

        //public ExternLibraryTagProvider()
        //{

        //}

        public Dictionary<string, string> Tags { get; private set; } = new Dictionary<string, string>();

        public void Clear()
        {
            Tags = new Dictionary<string, string>();
        }

        public void Set(string path)
        {
            using (var mp3 = new Mp3File(path))
            {
                Id3Tag tag = mp3.GetTag(Id3TagFamily.FileStartTag);

                Tags.Add("Album", tag.Album.Value);
                Tags["Artist"] = tag.Artists.Value;

                StringBuilder sb = new StringBuilder();
                foreach(var value  in tag.Comments)
                {
                    sb.Append(value.Comment).Append("|");
                }
                Tags["Comments"] = sb.ToString();

                Tags["TrackNo"] = tag.Track.Value;

                Tags["DateReleased"] = tag.Year.Value;
            }
        }
    }
}