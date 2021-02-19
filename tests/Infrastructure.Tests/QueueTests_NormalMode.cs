﻿using Domain.DataModel;
using Domain.DataModel.Queue;
using Infrastructure.SongQueue;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Infrastructure.Tests
{
    public class QueueTests_NormalMode
    {
        private static List<Song> first, second, third;

        public QueueTests_NormalMode()
        {
            initializeLists();
        }

        [Fact]
        public void ToArray_ShouldEqual()
        {
            ISongQueue queue = new Queue(first);
            Song[] actual = queue.ToArray();
            Song[] expected = first.ToArray();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Insert_InsertingAnElement_ShouldEqual()
        {
            //arrange 

            Song song = new Song
            {
                ID = 30,
                Name = "For Whom the Bell Tolls"
            };

            //list 3
            List<Song> list = new List<Song>
            {
                new Song
                {
                    ID = 11,
                    Name = "Kitty Later"
                },
                new Song
                {
                    ID = 12,
                    Name = "Ashtray Heart"
                },
                song,
                new Song
                {
                    ID = 13,
                    Name = "Battle for the Sun"
                },
                new Song
                {
                    ID = 14,
                    Name = "For What It's worth"
                },
                new Song
                {
                    ID = 15,
                    Name = "Devil in the Details"
                }
            };

            ISongQueue queue = new Queue(third);



            //act
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
            //arrange 

            List<Song> toBeInserted = new List<Song>
            {
                new Song
                {
                    ID = 31,
                    Name = "Getting Even"
                },
                new Song
                {
                    ID = 32,
                    Name = "Rituals"
                }
            };

            List<Song> list = new List<Song>
            {

                new Song
                {
                    ID = 6,
                    Name = "Hold on to Me"
                },
                new Song
                {
                    ID = 7,
                    Name = "Rob the Bank"
                },
                new Song
                {
                    ID = 8,
                    Name = "A Million Little Places"
                },

                toBeInserted[0],
                toBeInserted[1],

                new Song
                {
                    ID = 9,
                    Name = "Exit Wounds"
                },
                new Song
                {
                    ID = 10,
                    Name = "Purify"
                }
            };

            ISongQueue queue = new Queue(second);

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
        public void Jump_RemoveAllToEndAndInsertRange_ShoudlEqual()
        {
            ISongQueue queue = new Queue(first);

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
            ISongQueue queue = new Queue(second);

            Song song = new Song
            {
                ID = 11,
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
            ISongQueue queue = new Queue(second);

            queue.Clear();

            Assert.Empty(queue.ToArray());
        }

        [Fact]
        public void Set_ShouldEqual()
        {
            ISongQueue queue = new Queue(third);

            queue.Set(first);

            Song[] expected = first.ToArray();
            Song[] actual = queue.ToArray();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Current_ShouldReturnCurrentElement()
        {
            ISongQueue queue = new Queue(third);

            Song expected1 = new Song
            {
                ID = 11,
                Name = "Kitty Later"
            };

            Song expected2 = new Song
            {
                ID = 14,
                Name = "For What It's worth"
            };

            Song expected3 = new Song
            {
                ID = 12,
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
            ISongQueue queue = new Queue(second);

            Song actual1 = queue.Next;
            Song expected1 = new Song
            {
                ID = 6,
                Name = "Hold on to Me"
            };

            Song actual2 = queue.Next;
            Song expected2 = new Song
            {
                ID = 7,
                Name = "Rob the Bank"
            };

            Assert.Equal(expected1, actual1);
            Assert.Equal(expected2, actual2);


        }

        [Fact]
        public void Previous_FewTimes_ShouldReturnPrevious()
        {
            ISongQueue queue = new Queue(first);

            Song expected1 = new Song
            {
                ID = 1,
                Name = "Sweet Home Alabama"
            };

            Song expected2 = new Song
            {
                ID = 3,
                Name = "Loud Like Love"
            };

            Song expected3 = new Song
            {
                ID = 2,
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

        //throw some exceptions with next, previous, hasnext, hasprevious
        //push sequence to the limit
        //or the beginnig

        private void initializeLists()
        {
            first = new List<Song>
            {
                new Song
                {
                    ID = 1,
                    Name = "Sweet Home Alabama"
                },
                new Song
                {
                    ID = 2,
                    Name = "Processed Beats"
                },
                new Song
                {
                    ID = 3,
                    Name = "Loud Like Love"
                },
                new Song
                {
                    ID = 4,
                    Name = "Scene of the Crime"
                },
                new Song
                {
                    ID = 5,
                    Name = "Too Many Friends"
                }
            };

            second = new List<Song>
            {
                new Song
                {
                    ID = 6,
                    Name = "Hold on to Me"
                },
                new Song
                {
                    ID = 7,
                    Name = "Rob the Bank"
                },
                new Song
                {
                    ID = 8,
                    Name = "A Million Little Places"
                },
                new Song
                {
                    ID = 9,
                    Name = "Exit Wounds"
                },
                new Song
                {
                    ID = 10,
                    Name = "Purify"
                }
            };

            third = new List<Song>
            {
                new Song
                {
                    ID = 11,
                    Name = "Kitty Later"
                },
                new Song
                {
                    ID = 12,
                    Name = "Ashtray Heart"
                },
                new Song
                {
                    ID = 13,
                    Name = "Battle for the Sun"
                },
                new Song
                {
                    ID = 14,
                    Name = "For What It's worth"
                },
                new Song
                {
                    ID = 15,
                    Name = "Devil in the Details"
                }

            };

        }
    }
}