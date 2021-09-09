// using System;
// using System.Collections.Generic;
// using System.Linq;
// using Domain.Entities;
// using Domain.Queue;
// using Xunit;
//
// namespace Domain.Tests
// {
//     public class QueueTestsWithShuffle
//     {
//         private List<Song> TestData(int count = 10)
//         {
//             return Enumerable.Range(1, count).Select(x => new Song {Id = x}).ToList();
//         }
//         
//         [Fact]
//         public void ShuffleFromStart_OrderShouldBeRandom()
//         {
//             ISongQueue queue = new SongQueue();
//             var generated = TestData(18);
//             queue.Set(generated);
//             queue.Shuffle = true;
//             
//             var songs = new List<Song>();
//             while(queue.HasNext)
//                 songs.Add(queue.Next);
//             
//             var actual = songs.Select(x => x.Id);
//             var expected = generated.Select(x => x.Id);
//             
//             Assert.Equal(actual.Count(), expected.Count());
//             Assert.NotEqual(expected, actual);
//             Assert.Equal(expected, actual.OrderBy(x => x));
//         }
//
//         [Fact]
//         public void ShuffleInTheMiddle_HasNoPrevious()
//         {
//             ISongQueue queue = new SongQueue();
//             queue.Set(TestData(10));
//             
//             //skip three times
//             _ = queue.Next;
//             _ = queue.Next;
//             _ = queue.Next;
//
//             queue.Shuffle = true;
//             
//             Assert.False(queue.HasPrevious);
//             Assert.True(queue.HasNext);
//         }
//
//         [Fact]
//         public void ShuffleInTheMiddle_RemainingIndicesAreRandom()
//         {
//             ISongQueue queue = new SongQueue();
//             queue.Set(TestData(26));
//
//             for (var i = 0; i < 4; i++)
//                 _ = queue.Next;
//
//             queue.Shuffle = true;
//
//             var songs = new List<Song>();
//             while(queue.HasNext)
//                 songs.Add(queue.Next);
//
//             var actual = songs.Select(x => x.Id);
//             var expected = Enumerable.Range(5, 22).Select(Convert.ToInt64); //from 5 to 26 because first 4 were used
//             
//             Assert.Equal(expected.Count(), actual.Count());
//             Assert.NotEqual(expected, actual);
//             Assert.Equal(expected, actual.OrderBy(x => x));
//         }
//     }
// }