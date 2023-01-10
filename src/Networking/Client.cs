using System;

namespace Networking
{
    public class Client
    {
        public string Name { get; init; }
        public Guid Id { get; init; }
        public ClientPermissions Permissions { get; set; }

        public DateTime ConnectedOn { get; set; }
    }
}