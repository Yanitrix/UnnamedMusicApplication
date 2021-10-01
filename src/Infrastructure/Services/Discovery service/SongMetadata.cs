using System;
using System.Collections.Generic;

namespace Infrastructure.Services
{
    //holds every data written in file metadata
    //made basing on .mp3 file, gonna se what flac looks like
    //basically the same i think with flac, so i guess these is system-wide metadata
    public class SongMetadata
    {
        //file metadata
        public string Path { get; set; } //relative to root dir or absolute?
        public string FileName { get; set; }
        public string Extension { get; set; }
        public long SizeInBytes { get; set; }
        
        //song metadata
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Comments { get; set; } //or whatever additional information is possible, maybe dictionary<string, string> could be included
        public List<string> ContributingArtists { get; set; } //i guess its list because of the name
        public string Artist { get; set; } //i guess its only one because of the name
        public string Album { get; set; } //multiple possible? i dont think so
        //both are present in metadata 
        public int Year { get; set; }
        public DateTime DateReleased { get; set; } //dunno how that's one gonna be handled 
        public int TrackNo { get; set; }
        public string Genre { get; set; }
        public int DurationInSeconds { get; set; } //dunno if this or timespan, gonna see what's easier to read/use

        //in kbps
        public int BitRate { get; set; }
    }
}