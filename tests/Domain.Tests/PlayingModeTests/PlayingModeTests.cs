using System;
using Domain.Queue;
using Xunit;

namespace Domain.Tests.PlayingModeTests
{
    public class PlayingModeTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-4)]
        public void Initialize_CountNegativeOrZero_ExceptionThrown(int count)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new PlayingModeFake(count));
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(3, 4)]
        [InlineData(12, 17)]
        public void Initialize_CurrentGreaterOrEqualToCount_ThrowsException(int count, int current)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new PlayingModeFake(count, current));
        }

        [Theory]
        [InlineData(3, -2)]
        [InlineData(1, -3)]
        [InlineData(2, -2090)]
        public void Initialize_CurrentLowerThanMinusOne_ThrowsException(int count, int current)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new PlayingModeFake(count, current));
        }
        
        private class PlayingModeFake : PlayingMode
        {
            public PlayingModeFake(int count, int current = -1) : base(count, current)
            {
            }

            public override int Next { get; }
            public override bool HasNext { get; }
            public override int Count { get; set; }
            public override bool HasPrevious { get; }
            public override int Previous { get; }
            public override int Current { get; }
        }
    }
}