using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    /// <summary>
    /// Discovers all songs in given directory and inserts them into database.
    /// </summary>
    public interface IDiscoveryService
    {
        public DiscoveryReport Run(String[] rootDirectories, ComparisonMode comparisonMode);
    }
}
