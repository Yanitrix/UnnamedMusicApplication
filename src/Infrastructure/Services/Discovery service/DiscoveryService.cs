using Domain.DataAccess;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Discovery_service
{
    public class DiscoveryService : IDiscoveryService
    {
        private readonly IDatabase database;

        public DiscoveryService(IDatabase database)
        {
            this.database = database;
        }

        //TODO store all songs in a temporary table
        //so that bytes computing could be performed, and then separate it to other tables
        //just do that here
        
        //split methods into chunks, especially one that throw-catch exceptions
        public DiscoveryReport Run(string[] rootDirectories)
        {
            
            //what interests us is the artist and album
            //and i think that contributing artists can be left out and just treated as a tag
            //so no songs on album if the artist is a contributing artist
            //but definitely filtering songs by contributing artists
            //or maybe take all contributing artists from song in an album and include them in album data?
            
            //store data in tmp db file because of memory usage
            var connection = new LiteDatabase(Environment.CurrentDirectory + "temp.db");
            
            //read all songs and then insert them into a temp table
            var songs = ReadSongs(rootDirectories);
            var collection = connection.GetCollection<SongMetadata>();
            collection.InsertBulk(songs, songs.Count); //only more than 5000?

            //select all duplicates
            var query = "SELECT Title, Artist, COUNT(Path) from SongDto group by Title, Artist, having COUNT(Path) > 1";
            var duplicates = collection.Find(BsonExpression.Create(query)).ToList(); //because of possible multiple enumeration
            
            //now split the ienumerable into chunks basing on path
            //afaik theres not ext method to do that so im gonna do classic foreach
            var lastPath = "";
            foreach (var i in query)
            {
            }
            //and if any exception is thrown this temp file needs to be cleaned up
            //and force GC after this whole operation?
            var report = new DiscoveryReport();

            foreach(var i in rootDirectories)
            {
                //doing it in chunks to free some memory maybe

                //now they should be added in some way while avoiding duplicates
                //but i think that only matters for the one algorithm and not for the other.
                //so that should be the role of the algorithm to ensure that there are no duplicates

                //transaction should be made so that when anything happen we're gonna roll it back
                //but we need some iow for that.
                //scheiße
                database.BeginTransaction();

                //do stuff here
                database.CommitTransaction();
            }

            var artistCount = database.Artists.Count();
            var albumCount = database.Albums.Count();
            var songsCount = database.Songs.Count();

            return report;
        }

        private List<SongMetadata> ReadSongs(string[] dirs)
        {
            return new();
        }
    }
}
