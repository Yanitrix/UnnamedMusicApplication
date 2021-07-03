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
        [InlineData(20)]
        //initialize and step the whole sequence, without changing anything
        public void Next_GetWholeSequence_ShouldBeLinear(int count)
        {
            var mode = new LinearPlayingMode(count);

            List<int> expected = Enumerable.Range(begin + 1, amount).ToList();
            List<int> actual = new();
            while (mode.HasNext())
                actual.Add(mode.Next());

            Assert.Equal(amount, actual.Count);
            Assert.Equal(expected, actual);
        }

        [Theory]
        //check exceptions for wrong inputs
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
        //step through the whole and check if has next
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
        //step the whole and expect an exception
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

        [Fact]
        //initialize, then insert somewhere at different stages, walk through sequence
        public void Insert_GetWholeSequence_ShouldBeLinear()
        {

        }

        [Fact]
        public void Insert_WrongInput_ShouldThrowException()
        {

        }

        [Fact]
        //initialize, remove somewhere at different stages, walk through sequence
        public void Remove_GetWholeSequence_ShouldBeLinear()
        {

        }

        [Fact]
        public void Remove_WrongInput_ShouldThrowException()
        {

        }
    }
}
