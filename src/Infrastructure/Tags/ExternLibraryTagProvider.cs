using Id3;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Immutable;
using Domain.Exceptions;

namespace Infrastructure.Tags
{
    public class ExternLibraryTagProvider : ITagProvider
    {
        public IReadOnlyDictionary<string, string> ReadTags(string filePath)
        {
            var tags = new Dictionary<String, String>();

            if (!filePath.EndsWith(".mp3")) throw new UnsupportedFileFormatException("Unsupported file format. Must be .mp3");

            using (var mp3 = new Mp3File(filePath))
            {
                Id3Tag tag = mp3.GetTag(Id3TagFamily.FileStartTag);

                tags.Add("Album", tag.Album.Value);
                tags["Artist"] = tag.Artists.Value;

                StringBuilder sb = new StringBuilder();
                foreach (var value in tag.Comments)
                {
                    sb.AppendLine(value.Comment);
                }
                tags["Comments"] = sb.ToString();

                tags["TrackNo"] = tag.Track.Value;

                tags["DateReleased"] = tag.Year.Value;
            }

            return tags.ToImmutableDictionary();
        }

    }
}
