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

        public static IList<T> ListFor<T>(this Random random, int min = MinForList, int max = MaxForList)
            where T : new()
        {
            var list = new List<T>();
            Enumerable.Range(1, random.Next(min, max)).ForEach(x => list.Add(new T()));
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
    }


}