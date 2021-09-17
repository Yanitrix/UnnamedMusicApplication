using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Queue;
using FluentAssertions;
using Xunit;

namespace Domain.Tests
{
    public class QueueTestsWithShuffle
    {
        private static List<Song> TestData(int count)
        {
            return Enumerable.Range(1, count).Select(x => new Song {Id = x}).ToList();
        }
        
        [Fact]
        public void ShuffleFromStart_OrderShouldBeRandom()
        {
            ISongQueue queue = new SongQueue();
            var initial = TestData(18);
            queue.Set(initial);
            
            //act
            var traversed = new List<Song>();
            queue.Shuffle = true;
            while(queue.HasNext)
                traversed.Add(queue.Next);
            
            //assert
            traversed.Count.Should().Be(initial.Count);
            traversed.Should().NotEqual(initial);
            traversed.OrderBy(x => x.Id).Should().Equal(initial);
        }

        [Fact]
        public void ShuffleInTheMiddle_HasNoPrevious_FirstElementIsWhereShuffleStarted()
        {
            ISongQueue queue = new SongQueue();
            var initial = TestData(10);
            queue.Set(initial);
            
            //act
            _ = queue.Next;
            _ = queue.Next;
            var expected = queue.Next;
            queue.Shuffle = true;
            
            //asset
            Assert.False(queue.HasPrevious);
            Assert.True(queue.HasNext);
            //started shuffle on third index so it should be the first
            queue.Content[0].Should().Be(expected);
        }

        [Fact]
        public void ShuffleInTheMiddle_WholeQueueIsRandom()
        {
            ISongQueue queue = new SongQueue();
            var initial = TestData(26);
            queue.Set(initial);

            //act
            var actual = new List<Song>();
            for (var i = 0; i < 4; i++)
                _ = queue.Next;
            
            queue.Shuffle = true;
            while(queue.HasNext)
                actual.Add(queue.Next);

            //assert
            Assert.Equal(actual.Count, initial.Count);
            actual
                .Should()
                .NotEqual(initial);
            actual.OrderBy(x => x.Id)
                .Should()
                .Equal(initial);
        }

        [Fact]
        public void Shuffle_TraverseWhole_SequenceIsTheSameAsContentProperty()
        {
            //arrange
            ISongQueue queue = new SongQueue();
            var initial = TestData(113);
            queue.Set(initial);
            queue.Shuffle = true;
            var traversed = new List<Song>();
            IList<Song> content;

            //act
            content = queue.Content.ToList();
            while(queue.HasNext)
                traversed.Add(queue.Next);

            //assert
            //also check if the first original is the first on the list
            content.Should().Equal(traversed);
            content.Should().NotEqual(initial);
            content.Count.Should().Be(initial.Count);
            content.Count.Should().Be(traversed.Count);
            content
                .OrderBy(x => x.Id)
                .Should()
                .Equal(initial);
            traversed
                .OrderBy(x => x.Id)
                .Should()
                .Equal(initial);
            traversed[0].Should().Be(initial.First());
            content[0].Should().Be(initial.First());
        }

        [Fact]
        public void TurnShuffleOn_ThenOff_CurrentSongIsTheSameAsTheOneThatShuffleWasTurnedOffOn()
        {
            //arrange
            ISongQueue queue = new SongQueue();
            var initial = TestData(1200);
            queue.Set(initial);
            queue.Shuffle = true;
            
            //act
            for (int i = 0; i < 16; i++) //skip 16 in total and take the 17th
                _ = queue.Next;
            var expeted = queue.Next;
            queue.Shuffle = false;

            //assert
            queue.Content[queue.CurrentIndex].Should().Be(expeted);
            queue.Content.Count.Should().Be(initial.Count);
            queue.Content.Should().Equal(initial);
        }
    }
}