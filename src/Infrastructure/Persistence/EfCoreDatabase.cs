using Domain.Entities;
using LiteDB;
using System;
using System.IO;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class EfCoreDatabase : DbContext, Database
    {
        private const string DATABASE_NAME = "songs";
        private bool disposed;

        public DbSet<Song> Songs => this.Set<Song>();
        public DbSet<Artist> Artists => this.Set<Artist>();
        public DbSet<Playlist> Playlists => this.Set<Playlist>();
        public DbSet<Album> Albums => this.Set<Album>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
