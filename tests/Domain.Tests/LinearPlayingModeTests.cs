using Domain.Exceptions;
using Domain.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Domain.Tests
{
    public class LinearPlayingModeTests
    {
        [Theory]
        [InlineData(3, 20)]
        [InlineData(4, 15)]
        [InlineData(12, 27)]
        [InlineData(3, 4)]
        [InlineData(0, 1)]
        [InlineData(-1, 3)]
        public void Next_GetWholeSequence_ShouldBeLinear(int begin, int last)
        {
            var mode = new LinearPlayingMode();
            mode.Initialize(begin, last);

            int amount = last - begin;
            List<int> expected = Enumerable.Range(begin + 1, amount).ToList();
            List<int> actual = new();
            while (mode.HasNext())
                actual.Add(mode.Next());

            Assert.Equal(amount, actual.Count);
            Assert.Equal(expected, actual);
        }

        //edge cases
        [Theory]
        [InlineData(4, 3)]
        [InlineData(0, -1)]
        public void Initialize_WrongInput_ShouldThrowException(int begin, int last)
        {
            var mode = new LinearPlayingMode();

            void throwable()
            {
                mode.Initialize(begin, last);
            }

            Assert.Throws<ArgumentException>(throwable);
        }

        [Fact]
        public void HasNext_NextDoesNotExist_ReturnsFalse()
        {
            int begin = 3, last = 4;

            var mode = new LinearPlayingMode();
            mode.Initialize(begin, last);
            mode.Next();

            var hasNext = mode.HasNext();
            Assert.False(hasNext);
        }

        [Fact]
        public void Next_NextDoesNotExist_ThrowsException()
        {
            int begin = 3, last = 4;

            var mode = new LinearPlayingMode();
            mode.Initialize(begin, last);
            mode.Next();

            void throwable()
            {
                mode.Next();
            }

            Assert.Throws<NoItemAvailableException>(throwable);
        }
    }
}
