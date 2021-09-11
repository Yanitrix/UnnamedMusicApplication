using System;
using Domain.Entities;

namespace Networking
{
    public class PlaylistsChangedEventArgs : EventArgs
    {
        public Playlist Updated { get; set; }
    }
}