using Domain.Exceptions;
using Domain.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Domain.Tests
{
    public class ShufflePlayingModeTests
    {
        [Theory]
        [InlineData(3, 20)]
        [InlineData(-1, 3)]
        [InlineData(12, 15)]
        [InlineData(3, 4)]
        public void Next_GetWholeSequence_ShouldBeDistinctAndLinearAfterSorting(int begin, int last)
        {
            var mode = new ShufflePlayingMode();
            mode.Initialize(begin, last);

            var amount = last - begin;
            var expectedSorted = Enumerable.Range(begin + 1, amount).ToList();
            
            List<int> actual = new();
            while (mode.HasNext())
                actual.Add(mode.Next());

            var actualSorted = actual.OrderBy(x => x);
            var actualDistinct = actual.Distinct();

            Assert.Equal(amount, actual.Count);
            Assert.Equal(amount, actualDistinct.Count());
            Assert.Equal(expectedSorted, actualSorted);
        }

        [Theory]
        [InlineData(5, 2)]
        [InlineData(1, 1)]
        public void Initialize_WrongInput_ShouldThrowException(int begin, int last)
        {
            var mode = new ShufflePlayingMode();

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

            var mode = new ShufflePlayingMode();
            mode.Initialize(begin, last);
            mode.Next();

            var hasNext = mode.HasNext();
            Assert.False(hasNext);
        }

        [Fact]
        public void Next_NextDoesNotExist_ThrowsException()
        {
            int begin = 3, last = 4;

            var mode = new ShufflePlayingMode();
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
