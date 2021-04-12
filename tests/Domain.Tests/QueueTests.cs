using Domain.Entities;
using Domain.Queue;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Domain.Tests
{
    public class QueueTests
    {
        private static List<Song> first, second, third;

        public QueueTests()
        {
            InitializeLists();
        }

        [Fact]
        public void ToArray_ShouldEqual()
        {
            ISongQueue queue = new SongQueue();
            queue.Set(first);
            Song[] actual = queue.ToArray();
            Song[] expected = first.ToArray();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Insert_InsertingAnElement_ShouldEqual()
        {
            //prepare 

            ISongQueue queue = new SongQueue();
            Song song = new Song
            {
                Id = 30,
                Name = "For Whom the Bell Tolls"
            };
            //list 3
            List<Song> list = new List<Song>
            {
                new Song
                {
                    Id = 11,
                    Name = "Kitty Later"
                },
                new Song
                {
                    Id = 12,
                    Name = "Ashtray Heart"
                },
                song,
                new Song
                {
                    Id = 13,
                    Name = "Battle for the Sun"
                },
                new Song
                {
                    Id = 14,
                    Name = "For What It's worth"
                },
                new Song
                {
                    Id = 15,
                    Name = "Devil in the Details"
                }
            };



            //act
            queue.Set(third);
            _ = queue.Next; //doing it two times, because for the first time "next" takes the first of the queue
            _ = queue.Next;
            queue.Insert(song);

            Song[] expected = list.ToArray();
            Song[] actual = queue.ToArray();
            //asserting

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Insert_InsertRange_ShouldEqual()
        {
            //prepare 

            ISongQueue queue = new SongQueue();
            List<Song> toBeInserted = new List<Song>
            {
                new Song
                {
                    Id = 31,
                    Name = "Getting Even"
                },
                new Song
                {
                    Id = 32,
                    Name = "Rituals"
                }
            };

            List<Song> list = new List<Song>
            {

                new Song
                {
                    Id = 6,
                    Name = "Hold on to Me"
                },
                new Song
                {
                    Id = 7,
                    Name = "Rob the Bank"
                },
                new Song
                {
                    Id = 8,
                    Name = "A Million Little Places"
                },

                toBeInserted[0],
                toBeInserted[1],

                new Song
                {
                    Id = 9,
                    Name = "Exit Wounds"
                },
                new Song
                {
                    Id = 10,
                    Name = "Purify"
                }
            };

            queue.Set(second);

            //act

            _ = queue.Next;
            _ = queue.Next;
            _ = queue.Next;

            queue.Insert(toBeInserted);

            //assert

            Song[] expected = list.ToArray();
            Song[] actual = queue.ToArray();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Jump_RemoveAllToEndAndInsertRange_ShouldEqual()
        {
            ISongQueue queue = new SongQueue();
            queue.Set(first);

            List<Song> toExpect = new List<Song>
            {
                first[0],
                first[1],
                second[0],
                second[1],
                second[2],
                second[3],
                second[4],
            };

            _ = queue.Next;
            _ = queue.Next;

            queue.Replace(second);

            Song[] expected = toExpect.ToArray();
            Song[] actual = queue.ToArray();


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Add_AddToEnd_ShouldEqual()
        {
            ISongQueue queue = new SongQueue();
            queue.Set(second);

            Song song = new Song
            {
                Id = 11,
                Name = "Kitty Later"
            };

            List<Song> list = second.ToList();
            list.Add(song);
            queue.Add(song);

            Song[] expected = list.ToArray();
            Song[] actual = queue.ToArray();

            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Clear_ShouldBeEmpty()
        {
            ISongQueue queue = new SongQueue();
            queue.Set(second);

            queue.Clear();

            Assert.Empty(queue.ToArray());
        }

        [Fact]
        public void Set_ShouldEqual()
        {
            ISongQueue queue = new SongQueue();
            queue.Insert(third);

            queue.Set(first);

            Song[] expected = first.ToArray();
            Song[] actual = queue.ToArray();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Current_ShouldReturnCurrentElement()
        {
            ISongQueue queue = new SongQueue();
            queue.Set(third);

            Song expected1 = new Song
            {
                Id = 11,
                Name = "Kitty Later"
            };

            Song expected2 = new Song
            {
                Id = 14,
                Name = "For What It's worth"
            };

            Song expected3 = new Song
            {
                Id = 12,
                Name = "Ashtray Heart"
            };

            _ = queue.Next;
            _ = queue.Next;
            Song actual1 = queue.Previous;

            _ = queue.Next;
            _ = queue.Next;
            Song actual2 = queue.Next;

            _ = queue.Previous;
            Song actual3 = queue.Previous;

            Assert.Equal(expected1, actual1);
            Assert.Equal(expected2, actual2);
            Assert.Equal(expected3, actual3);
        }

        [Fact]
        public void Next_FewTimes_ShouldReturnNext()
        {
            ISongQueue queue = new SongQueue();
            queue.Set(second);

            Song actual1 = queue.Next;
            Song expected1 = new Song
            {
                Id = 6,
                Name = "Hold on to Me"
            };

            Song actual2 = queue.Next;
            Song expected2 = new Song
            {
                Id = 7,
                Name = "Rob the Bank"
            };

            Assert.Equal(expected1, actual1);
            Assert.Equal(expected2, actual2);


        }

        [Fact]
        public void Previous_FewTimes_ShouldReturnPrevious()
        {
            ISongQueue queue = new SongQueue();
            queue.Set(first);

            Song expected1 = new Song
            {
                Id = 1,
                Name = "Sweet Home Alabama"
            };

            Song expected2 = new Song
            {
                Id = 3,
                Name = "Loud Like Love"
            };

            Song expected3 = new Song
            {
                Id = 2,
                Name = "Processed Beats"
            };

            _ = queue.Next;
            _ = queue.Next;

            Song actual1 = queue.Previous;

            _ = queue.Next;
            _ = queue.Next;
            _ = queue.Next;
            _ = queue.Next;
            _ = queue.Previous;

            Song actual2 = queue.Previous;
            Song actual3 = queue.Previous;

            Assert.Equal(expected1, actual1);
            Assert.Equal(expected2, actual2);
            Assert.Equal(expected3, actual3);
        }


        private void InitializeLists()
        {
            first = new List<Song>
            {
                new Song
                {
                    Id = 1,
                    Name = "Sweet Home Alabama"
                },
                new Song
                {
                    Id = 2,
                    Name = "Processed Beats"
                },
                new Song
                {
                    Id = 3,
                    Name = "Loud Like Love"
                },
                new Song
                {
                    Id = 4,
                    Name = "Scene of the Crime"
                },
                new Song
                {
                    Id = 5,
                    Name = "Too Many Friends"
                }
            };

            second = new List<Song>
            {
                new Song
                {
                    Id = 6,
                    Name = "Hold on to Me"
                },
                new Song
                {
                    Id = 7,
                    Name = "Rob the Bank"
                },
                new Song
                {
                    Id = 8,
                    Name = "A Million Little Places"
                },
                new Song
                {
                    Id = 9,
                    Name = "Exit Wounds"
                },
                new Song
                {
                    Id = 10,
                    Name = "Purify"
                }
            };

            third = new List<Song>
            {
                new Song
                {
                    Id = 11,
                    Name = "Kitty Later"
                },
                new Song
                {
                    Id = 12,
                    Name = "Ashtray Heart"
                },
                new Song
                {
                    Id = 13,
                    Name = "Battle for the Sun"
                },
                new Song
                {
                    Id = 14,
                    Name = "For What It's worth"
                },
                new Song
                {
                    Id = 15,
                    Name = "Devil in the Details"
                }

            };

        }
    }
}
