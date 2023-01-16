using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public interface Database : IDisposable
    {
        public DbSet<Song> Songs { get; }

        public DbSet<Artist> Artists { get; }

        public DbSet<Playlist> Playlists { get; }

        public DbSet<Album> Albums { get; }
    }
}
