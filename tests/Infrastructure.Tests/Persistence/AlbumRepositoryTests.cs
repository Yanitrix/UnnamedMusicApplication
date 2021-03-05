using Domain.DataAccess;
using Domain.Entities;
using Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Infrastructure.Tests.Persistence
{
    public class AlbumRepositoryTests : DatabaseTestBase
    {
        private IAlbumRepository repo;

        public AlbumRepositoryTests()
        {
            this.repo = new AlbumRepository(this.database);
        }

        private Album[] TestData()
        {
            var a1songs = new Song[]
            {
                new()
                {
                    Name = "To lose my life",
                    Path = "root/to_lose_my_life",
                    Duration = TimeSpan.FromSeconds(120),
                    DateReleased = new DateTime(2001, 01, 01)
                },

                new()
                {
                    Name = "Death",
                    Path = "root/death",
                    Duration = TimeSpan.FromSeconds(240),
                    DateReleased = new DateTime(2001, 01, 02)
                },

                new()
                {
                    Name = "Take it out on me",
                    Path = "root/take_it_out_on_me",
                    Duration = TimeSpan.FromSeconds(360),
                    DateReleased = new DateTime(2001, 01, 03)
                },
            };

            var a2songs = new Song[]
            {
                new()
                {
                    Name = "Runnin up that hill",
                    Path = "root/running",
                    Duration = TimeSpan.FromSeconds(120),
                    DateReleased = new(1999, 01, 01)
                },

                new()
                {
                    Name = "Pure morning",
                    Path = "root/pure_morning",
                    Duration = TimeSpan.FromSeconds(240),
                    DateReleased = new(1999, 01, 02)
                },
            };

            var a1 = new Album
            {
                Name = "first album",
                DateReleased = new(1990, 01, 01),
            };

            var a2 = new Album
            {
                Name = "second album",
                DateReleased = new(1980, 12, 12)
            };

            foreach (var i in a1songs)
                a1.Songs.Add(i);

            foreach (var i in a2songs)
                a2.Songs.Add(i);

            return new Album[] { a1, a2 };
        }

        private void InsertData()
        {
            var albums = TestData();

            repo.Add(albums);
        }

        //the only one interesting me is getting by name with proper navigation properties
        [Fact]
        public void Add_ShouldExist()
        {
            var data = TestData();

            repo.Add(data);

            var albums = database.GetCollection<Album>();
            var songs = database.GetCollection<Song>();

            Assert.Equal(2, albums.Count());
            Assert.Equal(5, songs.Count());
        }

        [Fact]
        public void All_SongsShouldBeIncluded()
        {
            InsertData();

            var albums = repo.All();

            Assert.NotEmpty(albums);
            foreach (var i in albums)
                Assert.NotEmpty(i.Songs);
        }

        [Fact]
        public void GetByName_SongsShouldBeIncluded()
        {
            InsertData();

            var album = repo.GetByName("first");

            Assert.Single(album);
            Assert.Equal(3, album.First().Songs.Count);
        }
    }
}
