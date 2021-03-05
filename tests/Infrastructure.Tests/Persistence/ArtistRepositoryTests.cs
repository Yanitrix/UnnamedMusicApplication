using Domain.DataAccess;
using Domain.Entities;
using Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Tests.Persistence
{
    public class ArtistRepositoryTests : DatabaseTestBase
    {
        private IArtistRepository repo;

        public ArtistRepositoryTests()
        {
            repo = new ArtistRepository(this.database);
        }

        private Artist[] TestData()
        {
            return new Artist[]
            {
                new()
                {
                    Name = "artist 1",
                    Albums = new List<Album>
                    {
                        new()
                        {
                            Name = "album1",
                            Songs = new List<Song>
                            {
                                new()
                                {
                                    Name = "song1",
                                    Path = "root/song1",
                                    DateReleased = new(2001, 01, 01),
                                    Duration = TimeSpan.FromSeconds(100),
                                },

                                new()
                                {
                                    Name = "song2",
                                    Path = "root/song2",
                                    DateReleased = new(2001, 01, 02),
                                    Duration = TimeSpan.FromSeconds(200),
                                },
                            }
                        },

                        new()
                        {
                            Name = "album2",
                            Songs = new List<Song>
                            {
                                new()
                                {
                                    Name = "song3",
                                    Path = "root/song3",
                                    DateReleased = new(2001, 01, 03),
                                    Duration = TimeSpan.FromSeconds(300),
                                },

                                new()
                                {
                                    Name = "song4",
                                    Path = "root/song4",
                                    DateReleased = new(2001, 01, 04),
                                    Duration = TimeSpan.FromSeconds(400),
                                },

                                new()
                                {
                                    Name = "song5",
                                    Path = "root/song5",
                                    DateReleased = new(2001, 01, 05),
                                    Duration = TimeSpan.FromSeconds(500),
                                },
                            }
                        }
                    }
                },

                new()
                {
                    Name = "artist 2",
                    Albums = new List<Album>
                    {
                        new()
                        {
                            Name = "album1",
                            Songs = new List<Song>
                            {
                                new()
                                {
                                    Name = "song6",
                                    Path = "root/song6",
                                    DateReleased = new(2001, 01, 11),
                                    Duration = TimeSpan.FromSeconds(110),
                                },

                                new()
                                {
                                    Name = "song7",
                                    Path = "root/song7",
                                    DateReleased = new(2001, 01, 12),
                                    Duration = TimeSpan.FromSeconds(120),
                                },
                            }
                        },

                        new()
                        {
                            Name = "album2",
                            Songs = new List<Song>
                            {
                                new()
                                {
                                    Name = "song8",
                                    Path = "root/song8",
                                    DateReleased = new(2001, 01, 13),
                                    Duration = TimeSpan.FromSeconds(130),
                                },

                                new()
                                {
                                    Name = "song9",
                                    Path = "root/song9",
                                    DateReleased = new(2001, 01, 14),
                                    Duration = TimeSpan.FromSeconds(140),
                                },

                                new()
                                {
                                    Name = "song10",
                                    Path = "root/song10",
                                    DateReleased = new(2001, 01, 15),
                                    Duration = TimeSpan.FromSeconds(150),
                                },
                            }
                        },

                        new()
                        {
                            Name = "album3",
                            Songs = new List<Song>
                            {
                                new()
                                {
                                    Name = "song11",
                                    Path = "root/song11",
                                    DateReleased = new(2001, 01, 16),
                                    Duration = TimeSpan.FromSeconds(160),
                                },

                                new()
                                {
                                    Name = "song12",
                                    Path = "root/song12",
                                    DateReleased = new(2001, 01, 17),
                                    Duration = TimeSpan.FromSeconds(170),
                                },

                                new()
                                {
                                    Name = "song13",
                                    Path = "root/song13",
                                    DateReleased = new(2001, 01, 18),
                                    Duration = TimeSpan.FromSeconds(180),
                                },

                                new()
                                {
                                    Name = "song14",
                                    Path = "root/song14",
                                    DateReleased = new(2001, 01, 19),
                                    Duration = TimeSpan.FromSeconds(190),
                                },
                            }
                        }
                    }
                },
            };
        }

        private void InsertData()
        {
            repo.Add(TestData());
        }

        [Fact]
        public void Add_ChildrenEntitiesShouldExist()
        {
            var data = TestData();

            repo.Add(data);

            var artists = database.GetCollection<Artist>().FindAll();
            var albums = database.GetCollection<Album>().FindAll();
            var songs = database.GetCollection<Song>().FindAll();

            Assert.Equal(2, artists.Count());
            Assert.Equal(5, albums.Count());
            Assert.Equal(14, songs.Count());
        }

        [Fact]
        public void GetByName_ReturnsProperEntity_ChildrenLoaded()
        {
            InsertData();

            var artist = repo.GetByName("artist 1").FirstOrDefault();

            Assert.NotNull(artist);
            Assert.Equal(2, artist.Albums.Count);
            Assert.Equal(2, artist.Albums[0].Songs.Count);
            Assert.Equal(3, artist.Albums[1].Songs.Count);
        }

        [Fact]
        public void GetAll_ChildrenLoaded()
        {
            InsertData();

            var artists = repo.All().ToList();

            Assert.Equal(2, artists.Count);
            Assert.Equal(2, artists[0].Albums.Count);
            Assert.Equal(3, artists[1].Albums.Count);
            Assert.NotEmpty(artists[0].Albums[0].Songs);
            Assert.NotEmpty(artists[0].Albums[1].Songs);
            Assert.NotEmpty(artists[1].Albums[0].Songs);
            Assert.NotEmpty(artists[1].Albums[1].Songs);
            Assert.NotEmpty(artists[1].Albums[2].Songs);
        }
    }
}
