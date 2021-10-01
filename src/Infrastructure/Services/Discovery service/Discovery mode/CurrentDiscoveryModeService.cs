namespace Infrastructure.Services
{
    public class CurrentDiscoveryModeService
    {
        public DiscoveryMode CurrentDiscoveryMode { get; set; }
        public IDiscoveryService DiscoveryService { get; set; }
    }
}