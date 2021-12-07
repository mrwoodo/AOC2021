using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2021
{
    public class Day07 : DayBase, ITwoPartQuestion
    {
        public List<int> Crabs = new List<int>();
        private readonly Dictionary<int, int> CachedFactors = new Dictionary<int, int>();

        public Day07()
        {
            Crabs = (from line in this.InputFile.Split(",")
                     select int.Parse(line)).ToList();

            Run(() => Part1(), () => Part2());
        }

        public string Part1()
        {
            var BestFuelUsage = int.MaxValue;

            for (int i = 0; i < Crabs.Count; i++)
            {
                var fuelCost = 0;

                for (int j = 0; j < Crabs.Count; j++)
                    fuelCost += Math.Abs(Crabs[j] - Crabs[i]);

                if (fuelCost < BestFuelUsage)
                    BestFuelUsage = fuelCost;
            }

            return $"{BestFuelUsage}";
        }

        public string Part2()
        {
            var BestFuelUsage = int.MaxValue;

            for (int i = Crabs.Min(); i <= Crabs.Max(); i++)
            {
                var fuelCost = 0;

                for (int j = 0; j < Crabs.Count; j++)
                    fuelCost += CalculateCost(Math.Abs(Crabs[j] - i));

                if (fuelCost < BestFuelUsage)
                    BestFuelUsage = fuelCost;
            }

            return $"{BestFuelUsage}";
        }

        private int CalculateCost(int num)
        {
            var result = 0;

            //no point calculating this every time, it gets reused a lot
            if (CachedFactors.ContainsKey(num))
                result = CachedFactors[num];
            else
            {
                result = Enumerable
                    .Range(1, num)
                    .Aggregate(1, (x, y) => x + y) - 1;

                CachedFactors[num] = result;
            }

            return result;
        }
    }
}