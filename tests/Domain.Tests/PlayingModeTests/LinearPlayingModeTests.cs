using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Queue;
using Xunit;
// ReSharper disable HeapView.ObjectAllocation
// ReSharper disable HeapView.BoxingAllocation

namespace Domain.Tests.PlayingModeTests
{
    public class LinearPlayingModeTests
    {
        private PlayingMode sut;
        
        [Theory]
        [InlineData(4, 3)]
        [InlineData(5, 4)]
        [InlineData(114, 113)]
        public void Initialize_CountOneGreaterThanCount_SequenceIsEmpty_ThrowsException(int count, int current)
        {
            Assert.Throws<InvalidOperationException>(() => new LinearPlayingMode(count, current));
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(43)]
        [InlineData(123)]
        public void TraverseWhole_DifferentLengths_IsLinear(int count)
        {
            //arrange
            sut = new LinearPlayingMode(count);
            var expected = FromStartToEnd(0, count - 1);

            //act
            var actual = new List<int>();
            while(sut.HasNext)
                actual.Add(sut.Next);
            
            //assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(12, 2)]
        [InlineData(5, 2)]
        [InlineData(4, 1)]
        [InlineData(10, 0)]
        [InlineData(12, -1)]
        public void TraverseWhole_DifferentLengthsAndStarts_IsLinear(int count, int current)
        {
            sut = new LinearPlayingMode(count, current);
            var expected = FromStartToEnd(current + 1, count - 1);

            var actual = new List<int>();
            while(sut.HasNext)
                actual.Add(sut.Next);
            
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(5, 3)]
        [InlineData(8, 5)]
        [InlineData(12, 10)]
        public void SetCount_ToLower_CurrentDoesNotExceedLimit(int count, int newCount)
        {
            sut = new LinearPlayingMode(count);
            while (sut.HasNext)
                _ = sut.Next;
            
            Assert.Equal(count - 1, sut.Current);

            sut.Count = newCount;
            
            Assert.Equal(newCount - 1, sut.Current);
        }
        
        [Fact]
        public void EdgeCase_SetCountToZero_IsEmpty()
        {
            sut = new LinearPlayingMode(1); //doesnt really matter

            sut.Count = 0;
            Assert.False(sut.HasNext);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-12)]
        public void SetCountBelowZero_ThrowsException(int count)
        {
            sut = new LinearPlayingMode(1);

            Assert.Throws<ArgumentException>(() => sut.Count = count);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(8)]
        [InlineData(12)]
        public void Next_ExceedUpperBound_ThrowsException(int count)
        {
            sut = new LinearPlayingMode(count);
            foreach (var i in Range(count))
            {
                _ = sut.Next;
            }
            
            Assert.False(sut.HasNext);
            Assert.Throws<InvalidOperationException>(() => _ = sut.Next);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(8)]
        [InlineData(12)]
        public void Previous_ExceedLowerBound_ThrowsException(int count)
        {
            sut = new LinearPlayingMode(count, count - 2);
            foreach (var i in Range(count - 1))
            {
                _ = sut.Previous;
            }
            
            Assert.False(sut.HasPrevious);
            Assert.Throws<InvalidOperationException>(() => _ = sut.Previous);
        }
        
        

        private IEnumerable<int> FromStartToEnd(int first, int max)
        {
            for (int i = first; i <= max; i++)
                yield return i;
        }

        private IEnumerable<int> Range(int count) => Enumerable.Range(0, count);
    }
}