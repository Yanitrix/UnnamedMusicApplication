using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Queue
{
    public class ShuffledCollection
    {
        private int[] content;
        private int count;
        private int currentIndex;
        private static readonly Random rng = new Random();
        private readonly ArrayPool<int> pool = ArrayPool<int>.Shared;

        public ShuffledCollection(int count, int currentIndex = -1)
        {
            this.currentIndex = currentIndex;
            content = pool.Rent(count);
            for (int i = 0; i < count; i++)
                content[i] = i;
            Shuffle();
        }

        public int Next => content[++currentIndex];
        
        //TODO encapsulate those methods in some helper struct / extension methods
        public int Count => this.count - currentIndex - 1;

        public void Add(int data)
        {
            //insert randomly
            var index = rng.Next(currentIndex, count) + 1;
            count++;
            var tmp = content;
            content = pool.Rent(count);
            
            for(int i = 0; i < index; i++)
            {
                content[i] = tmp[i];
            }
            for(int i = index + 1; i < count; i++)
            {
                content[i + 1] = tmp[i];
            }
            content[index] = data;

            pool.Return(tmp);
        }

        public void Add(IEnumerable<int> data)
        {
            //after current
            var oldCount = count;
            var enumerable = data as int[] ?? data.ToArray();
            count += enumerable.Length;
            var tmp = content;
            content = pool.Rent(count);
            for(int i = 0; i < oldCount; i++)
            {
                content[i] = tmp[i];
            }
            var iterator = enumerable.GetEnumerator();
            for(int i = oldCount; iterator.MoveNext(); i++)
            {
                content[i] = (int)iterator.Current;
            }

            Shuffle();
            pool.Return(tmp, true);
        }

        public void Remove(Predicate<int> condition)
        {
            var removeCount = 0;
            //check for more than 256k songs
            Span<int> toRemove = stackalloc int[count];
            for(int i = 0; i <= currentIndex; i++)
            {
                if (condition(content[i]))
                {
                    toRemove[removeCount++] = i;
                }
            }

            var end = currentIndex;
            currentIndex = currentIndex - removeCount + 1;
            for(int i = end + 1; i < count; i++)
            {
                if (condition(content[i]))
                {
                    toRemove[removeCount++] = i;
                }
            }

            //now they can be actually removed
            Remove(toRemove, removeCount);
        }

        public void Clear()
        {
            pool.Return(content, true);
            content = null;
            currentIndex = -1;
        }

        public void Reset()
        {
            currentIndex = -1;
        }

        private void Remove(Span<int> indices, int count)
        {
            var tmp = content;
            content = pool.Rent(this.count - count);
            var current = 0;
            
            for(int i = 0; i < this.count; i++)
            {
                if (i == indices[current])
                {
                    if (indices[current] < currentIndex)
                    {
                        currentIndex--;
                    }

                    current++;
                    continue;
                }
                content[i - current] = tmp[i];
            }

            this.count -= count;
            pool.Return(tmp);
        }

        private void Shuffle()
        {
            var begin = currentIndex + 1;
            // var arr = new ArraySegment<int>(content, begin, this.count - begin);
            // int count = arr.Count;
            int count = this.count - begin;
            var arr = new ArraySegment<int>(content, begin, count);
            for (int current = 0; current < (count - 1); current++)
            {
                int random = current + rng.Next(count);
                int temp = arr[random];
                arr[random] = arr[current];
                arr[current] = temp;
            }
        }
    }
}
