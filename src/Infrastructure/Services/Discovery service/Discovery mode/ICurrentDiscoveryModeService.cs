using Infrastructure.Services.Discovery_service;

namespace Infrastructure.Services
{
    public interface ICurrentDiscoveryModeService
    {
        //get will get it from some file
        //set will write to that file
        DiscoveryMode CurrentDiscoveryMode { get; set; }

        IDiscoveryAlgorithm CurrentDiscoveryAlgorithm { get; }
    }
}
