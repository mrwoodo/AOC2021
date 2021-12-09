using System;
using System.Linq;
using System.Collections.Generic;

namespace AOC2021
{
    public class Day01 : DayBase, ITwoPartQuestion
    {
        private readonly List<int> input;
        IEnumerable<Tuple<int, int>> pairs;

        public Day01()
        {
            input = this.InputFileAsIntList;
            Run(() => Part1(), () => Part2());
        }

        public string Part1()
        {
            pairs = Enumerable.Zip(input, input.Skip(1), (a, b) => Tuple.Create(a, b));

            return $"{pairs.Where(i => i.Item1 < i.Item2).Count()}";
        }

        public string Part2()
        {
            var summed = pairs.Zip(input.Skip(2), (a, b) => a.Item1 + a.Item2 + b);
            var summedPairs = Enumerable.Zip(summed, summed.Skip(1), (a, b) => Tuple.Create(a, b));

            return $"{summedPairs.Where(i => i.Item1 < i.Item2).Count()}";
        }
    }
}