using Domain.DataAccess;
using Domain.Entities;
using Infrastructure.Persistence.Repositories;
using System;
using System.Linq;
using Xunit;

namespace Infrastructure.Tests.Persistence
{
    public class SongRepositoryTests : DatabaseTestBase
    {

        private readonly ISongRepository repo;

        private void InitData()
        {
            Song[] songs = {
                new()
                {
                    Name = "stick",
                    Path = "root/stick",
                    TrackNo = 1,
                    Duration = TimeSpan.FromSeconds(100),
                    DateReleased = new DateTime(2001, 01, 01),
                },

                new()
                {
                    Name = "sticky",
                    Path = "root/sticky",
                    TrackNo = 2,
                    Duration = TimeSpan.FromSeconds(200),
                    DateReleased = new DateTime(2001, 01, 02)
                },

                new()
                {
                    Name = "a name",
                    Path = "root/a_name",
                    TrackNo = 3,
                    Duration = TimeSpan.FromSeconds(300),
                    DateReleased = new DateTime(2001, 01, 03)
                },
                
                new()
                {
                    Name = "a longer Name",
                    Path = "root/a_longer_name",
                    TrackNo = 4,
                    Duration = TimeSpan.FromSeconds(400),
                    DateReleased = new DateTime(2001, 01, 04)
                },
                    
                new()
                {
                    Name = "an even longer NaMe",
                    Path = "root/an_even_longer_name",
                    TrackNo = 5,
                    Duration = TimeSpan.FromSeconds(500),
                    DateReleased = new DateTime(2001, 01, 05)
                },

                new()
                {
                    Name = "death",
                    Path = "root/death",
                    TrackNo = 6,
                    Duration = TimeSpan.FromSeconds(600),
                    DateReleased = new DateTime(2001, 01, 06)
                },
            };

            database.GetCollection<Song>().InsertBulk(songs);
        }

        public SongRepositoryTests()
        {
            repo = new SongRepository(this.database);
        }

        [Fact]
        public void AddSongs_SongExists()
        {
            Song[] songs = {
                new()
                {
                    Name = "name",
                    Path = "path",
                    TrackNo = 2,
                    Duration = TimeSpan.FromSeconds(240),
                    DateReleased = new DateTime(1999, 12, 1),
                },

                new()
                {
                    Name = "Name",
                    Path = "other path",
                    TrackNo = 3,
                    Duration = TimeSpan.FromSeconds(320),
                    DateReleased = new DateTime(1980, 1, 1)
                },

                new()
                {
                    Name = "Name",
                    Path = "other path",
                    TrackNo = 3,
                    Duration = TimeSpan.FromSeconds(320),
                    DateReleased = new DateTime(1980, 1, 1)
                },
            };

            repo.Add(songs);

            var actual = repo.All();

            Assert.NotEmpty(actual);
            Assert.Equal(3, actual.Count());
        }

        [Fact]
        public void GetSongById_ReturnsProperSong()
        {
            //i dont actually know if itll update the field after addition
            //it will

            Song[] songs = new Song[]
            {
                new()
                {
                    Name = "name",
                    Path = "path",
                    TrackNo = 2,
                    Duration = TimeSpan.FromSeconds(240),
                    DateReleased = new DateTime(1999, 12, 1),
                },

                new()
                {
                    Name = "Name",
                    Path = "other path",
                    TrackNo = 3,
                    Duration = TimeSpan.FromSeconds(320),
                    DateReleased = new DateTime(1980, 1, 1)
                },

                new()
                {
                    Name = "Other Name",
                    Path = "different path",
                    TrackNo = 12,
                    Duration = TimeSpan.FromSeconds(5670),
                    DateReleased = new DateTime(2001, 01, 01)
                },
            };

            database.GetCollection<Song>().InsertBulk(songs);
            var id = songs[2].Id;

            Assert.NotEqual(default(long), id);
            var song = repo.GetById(id);
            Assert.Equal(song.Name, songs[2].Name);
            Assert.Equal(song.Path, songs[2].Path);
            Assert.Equal(song.TrackNo, songs[2].TrackNo);
            Assert.Equal(song.Duration, songs[2].Duration);
        }

        [Theory]
        [InlineData("sticky")]
        [InlineData("death")]
        public void GetSongByExactName_ReturnsProperSong(string name)
        {
            InitData();

            var found = repo.GetByName(name);

            Assert.NotEmpty(found);
            Assert.Single(found);
            Assert.Equal(name, found.First().Name);
        }

        [Theory]
        [InlineData("stick", 2)] //should be two
        [InlineData("long", 2)] //two
        [InlineData("name", 3)] //three
        public void GetSongByNameSubstring_ReturnsAllWhoseTitleContainsGivenName(string name, int amount)
        {
            InitData();

            var found = repo.GetByName(name);

            Assert.NotEmpty(found);
            Assert.Equal(amount, found.Count());
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData(" \t\n")]
        [InlineData("\t\n")]
        [InlineData("\r")]
        [InlineData("\t")]
        public void GetSongByNameSubstring_NameNotFoundOrEmpty_ReturnsEmptyCollection(string name)
        {
            InitData();

            var found = repo.GetByName(name);

            Assert.Empty(found);
        }

        [Fact]
        public void AddSong_DateTimeAndDurationAreWrittenProperly()
        {
            var song = new Song
            {
                Name = "name",
                Path = "root/name",
                Duration = TimeSpan.FromSeconds(120),
                DateReleased = new(2001, 01, 01)
            };
            
            repo.Add(song);
            var actual = repo.GetById(song.Id);
            
            Assert.Equal(actual.DateReleased, new DateTime(2001, 01, 01));
            Assert.Equal(actual.Duration, new TimeSpan(0, 0, 120));
        }


    }
}
