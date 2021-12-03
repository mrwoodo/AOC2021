using System.Collections.Generic;
using System.Linq;

namespace AOC2021
{
    public class Day02 : DayBase, ITwoPartQuestion
    {
        public Day02()
        {
            Run(() => Part1(), () => Part2());
        }

        public string Part1()
        {
            var movements = new Dictionary<string, int>();

            movements["down"] = 0;
            movements["up"] = 0;
            movements["forward"] = 0;

            foreach (var s in this.InputFileAsStringList)
            {
                var command = s.Split(' ');
                var direction = command[0];
                var amount = int.Parse(command[1]);

                movements[direction] += amount;
            }

            var horizontal = movements["forward"];
            var vertical = movements["down"] - movements["up"];

            return $"{horizontal * vertical}";
        }

        public string Part2()
        {
            int aim = 0;
            var horizontal = 0;
            var vertical = 0;

            foreach (var s in this.InputFileAsStringList)
            {
                var command = s.Split(' ');
                var direction = command[0];
                var amount = int.Parse(command[1]);

                if (direction == "down")
                    aim += amount;
                else if (direction == "up")
                    aim -= amount;
                else
                {
                    horizontal += amount;
                    vertical += aim * amount;
                }
            }

            return $"{horizontal * vertical}";
        }
    }
}