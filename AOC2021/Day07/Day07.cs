using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2021
{
    public class Day07 : DayBase, ITwoPartQuestion
    {
        public List<int> Crabs = new List<int>();
        private readonly Dictionary<int, int> Steps = new Dictionary<int, int>();

        public Day07()
        {
            Crabs = (from line in this.InputFile.Split(",")
                     select int.Parse(line)).ToList();

            Run(() => Part1(), () => Part2());
        }

        public string Part1()
        {
            var BestFuelCost = int.MaxValue;

            for (int i = 0; i < Crabs.Count; i++)
            {
                var FuelCost = 0;

                for (int j = 0; j < Crabs.Count; j++)
                    FuelCost += Math.Abs(Crabs[j] - Crabs[i]);

                if (FuelCost < BestFuelCost)
                    BestFuelCost = FuelCost;
            }

            return $"{BestFuelCost}";
        }

        public string Part2()
        {
            PreComputeFuelSteps();

            var BestFuelCost = int.MaxValue;

            for (int i = Crabs.Min(); i <= Crabs.Max(); i++)
            {
                var FuelCost = 0;

                for (int j = 0; j < Crabs.Count; j++)
                    FuelCost += Steps[Math.Abs(Crabs[j] - i)];

                if (FuelCost < BestFuelCost)
                    BestFuelCost = FuelCost;
            }

            return $"{BestFuelCost}";
        }

        private void PreComputeFuelSteps()
        {
            var counter = 0;

            for (int i = Crabs.Min(); i <= Crabs.Max(); i++)
            {
                counter += i;
                Steps[i] = counter;
            }
        }
    }
}