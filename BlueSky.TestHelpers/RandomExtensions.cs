using System;
using System.Collections.Generic;
using System.Linq;
using BlueSky.Common;

namespace BlueSky.TestHelpers
{
    public static class RandomExtensions
    {
        public const int MinForList = 10;
        public const int MaxForList = 20;

        public static IList<int> ExclusiveInts(this Random random, int count)
        {
            var list = new List<int>();
            Enumerable.Range(1, count).ForEach(x =>
            {
                var item = random.Next();
                while(list.Contains(item))
                {
                    item = random.Next();
                }
                list.Add(item);
            });
            return list;
        } 

        public static IList<T> FilledListFor<T>(this Random random, Action<T> action, int min = MinForList,
            int max = MaxForList)
            where T : new()
        {
            var list = random.ListFor<T>(min, max);
            list.ForEach(action);
            return list;
        }

        public static IList<T> ListFor<T>(this Random random, int min = MinForList, int max = MaxForList)
            where T : new()
        {
            var list = new List<T>();
            Enumerable.Range(1, random.Next(min, max)).ForEach(x => list.Add(new T()));
            return list;
        }

        public static decimal NextDecimal(this Random random)
        {
            var scale = (byte)random.Next(29);
            var sign = random.Next(2) == 1;
            return new decimal(random.Next(),
                               random.Next(),
                               random.Next(),
                               sign,
                               scale);
        }

        public static long NextLong(this Random random)
        {
            var buffer = new byte[sizeof(Int64)];
            random.NextBytes(buffer);
            return BitConverter.ToInt64(buffer, 0);
        }
    }
}