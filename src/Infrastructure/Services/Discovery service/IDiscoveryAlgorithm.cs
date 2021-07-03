using Domain.Entities;
using System.Collections.Generic;

namespace Infrastructure.Services.Discovery_service
{
    public interface IDiscoveryAlgorithm
    {
        IList<Artist> DiscoverArtists(string directoryPath);
    }
}
