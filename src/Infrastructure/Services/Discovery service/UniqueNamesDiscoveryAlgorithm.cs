using Domain.Entities;
using Infrastructure.Tags;
using System;
using System.Collections.Generic;

namespace Infrastructure.Services.Discovery_service
{
    public class UniqueNamesDiscoveryAlgorithm : IDiscoveryAlgorithm
    {
        private readonly ITagProvider tagProvider;

        public UniqueNamesDiscoveryAlgorithm(ITagProvider tagProvider)
        {
            this.tagProvider = tagProvider;
        }

        public IList<Artist> DiscoverArtists(string directoryPath)
        {
            throw new NotImplementedException();

            //get all songs, check artist and album names
            //split if multiple
            //avoid duplicates
        }
    }
}
