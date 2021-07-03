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
        private readonly ICurrentDiscoveryModeService modeService;
        private readonly IDatabase database;

        public DiscoveryService(ICurrentDiscoveryModeService modeService, IDatabase database)
        {
            this.modeService = modeService;
            this.database = database;
        }

        public DiscoveryReport Run(string[] rootDirectories)
        {
            var algorithm = modeService.CurrentDiscoveryAlgorithm;
            var report = new DiscoveryReport();

            foreach(var i in rootDirectories)
            {
                //doing it in chunks to free some memory maybe
                var artists = algorithm.DiscoverArtists(i);

                //now they should be added in some way while avoiding duplicates
                //but i think that only matters for the one algorithm and not for the other.
                //so that should be the role of the alogrithm to ensure that there are no duplicates

                //transaction should be made so that when anything happen we're gonna roll it back
                //but we need some iow for that.
                //scheisse
                database.BeginTransaction();

                database.Artists.Add(artists);

                database.CommitTransaction();
            }

            var artistCount = database.Artists.Count();
            var albumCount = database.Albums.Count();
            var songsCount = database.Songs.Count();
            //some way to get bytes

            return report;
        }
    }
}
