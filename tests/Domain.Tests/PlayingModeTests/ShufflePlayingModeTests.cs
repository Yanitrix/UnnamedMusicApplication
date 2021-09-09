using Domain.Queue;

namespace Domain.Tests.PlayingModeTests
{
    public class ShufflePlayingModeTests
    {
        private PlayingMode sut;

        public ShufflePlayingModeTests()
        {
            sut = new ShufflePlayingMode(0);
        }
    }
}