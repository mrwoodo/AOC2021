using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2021
{
    public class Day07 : DayBase, ITwoPartQuestion
    {
        public List<int> Crabs = new List<int>();
        private readonly Dictionary<int, int> Factors = new Dictionary<int, int>();

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
                var FuelCost = 0;

                for (int j = 0; j < Crabs.Count; j++)
                    FuelCost += Math.Abs(Crabs[j] - Crabs[i]);

                if (FuelCost < BestFuelUsage)
                    BestFuelUsage = FuelCost;
            }

            return $"{BestFuelUsage}";
        }

        public string Part2()
        {
            var BestFuelUsage = int.MaxValue;

            CalculateFactors();

            for (int i = Crabs.Min(); i <= Crabs.Max(); i++)
            {
                var FuelCost = 0;

                for (int j = 0; j < Crabs.Count; j++)
                    FuelCost += Factors[Math.Abs(Crabs[j] - i)];

                if (FuelCost < BestFuelUsage)
                    BestFuelUsage = FuelCost;
            }

            return $"{BestFuelUsage}";
        }

        private void CalculateFactors()
        {
            var counter = 0;

            for (int i = 0; i < 2000; i++)
            {
                counter += i;
                Factors[i] = counter;
            }
        }
    }
}