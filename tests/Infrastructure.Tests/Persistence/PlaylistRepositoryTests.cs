using Domain.DataAccess;
using Domain.Entities;
using Infrastructure.Persistence.Repositories;
using LiteDB;
using Xunit;

namespace Infrastructure.Tests.Persistence
{
    public class PlaylistRepositoryTests : DatabaseTestBase
    {
        private IPlaylistRepository repo;

        public PlaylistRepositoryTests()
        {
            repo = new PlaylistRepository(this.database);
        }

        [Fact]
        public void TryAddAnotherWithSameName_SeeWhatHappens()
        {
            var playlist = new Playlist
            {
                Name = "playlist"
            };
            
            var another = new Playlist
            {
                Name = "playlist"
            };
            
            repo.Add(playlist);
            
            void action() => repo.Add(another);

            Assert.Throws<LiteException>(action);
        }
    }
}