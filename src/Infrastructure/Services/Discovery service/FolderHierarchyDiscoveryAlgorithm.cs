using Domain.Entities;
using Infrastructure.Tags;
using System;
using System.Collections.Generic;

namespace Infrastructure.Services.Discovery_service
{
    public class FolderHierarchyDiscoveryAlgorithm : IDiscoveryAlgorithm
    {
        private readonly ITagProvider provider;

        public FolderHierarchyDiscoveryAlgorithm(ITagProvider provider)
        {
            this.provider = provider;
        }

        public IList<Artist> DiscoverArtists(string directoryPath)
        {
            throw new NotImplementedException();

            //iterate through the folders
            //treat subfolders as albums and files in them as songs
            //treat every loose song as a single
        }
    }
}
