using System.Collections.Generic;
using System.Linq;

namespace AOC2021
{
    public class Day09 : DayBase, ITwoPartQuestion
    {
        public readonly int[,] map = null;
        public int maxX = 0;
        public int maxY = 0;
        public List<(int x, int y)> LowPoints = new List<(int, int)>(); //Store the low points from part 1 for part 2
        public Dictionary<int, int> ColourCounters = new Dictionary<int, int>(); //Keep track of how many points are of a certain 'colour'

        public Day09()
        {
            var input = this.InputFileAsStringList;

            maxX = input[0].Length;
            maxY = input.Count;
            map = new int[maxX, maxY];

            for (int y = 0; y < maxY; y++)
                for (int x = 0; x < maxX; x++)
                    map[x, y] = int.Parse(input.ElementAt(y)[x].ToString());

            Run(() => Part1(), () => Part2());
        }

        public string Part1()
        {
            var LowPointValues = new List<int>();

            for (int y = 0; y < maxY; y++)
                for (int x = 0; x < maxX; x++)
                    if (LowPoint(x, y))
                    {
                        LowPointValues.Add(map[x, y] + 1);
                        LowPoints.Add((x, y));
                    }

            return $"{LowPointValues.Sum()}";
        }

        public string Part2()
        {
            var colourCounter = -1;
            foreach (var (x, y) in LowPoints)
            {
                Paint(x, y, colourCounter);
                colourCounter--; //We're going minus, to steer away from the 9s
            }

            var basins = ColourCounters.Values.OrderByDescending(i => i).Take(3); //3 largest basins

            return $"{basins.ElementAt(0) * basins.ElementAt(1) * basins.ElementAt(2)}";
        }

        private bool LowPoint(int x, int y)
        {
            var val = map[x, y];

            return ((val < PointValue(x - 1, y)) &&
                    (val < PointValue(x + 1, y)) &&
                    (val < PointValue(x, y - 1)) &&
                    (val < PointValue(x, y + 1)));
        }

        private int PointValue(int x, int y)
        {
            if ((x >= 0) && (x < maxX) &&
                (y >= 0) && (y < maxY))
                return map[x, y];

            return 99;
        }

        private void Paint(int x, int y, int colour)
        {
            if ((x < 0) || (x >= maxX) ||
                (y < 0) || (y >= maxY))
                return;

            if ((map[x, y] == 9) || //skip the highest bounds
                (map[x, y] == colour)) //already painted
                return;

            map[x, y] = colour; //paint this point
            if (!ColourCounters.ContainsKey(colour))
                ColourCounters[colour] = 0;

            ColourCounters[colour]++; //update the colour counter

            Paint(x + 1, y, colour);
            Paint(x - 1, y, colour);
            Paint(x, y + 1, colour);
            Paint(x, y - 1, colour);
        }
    }
}