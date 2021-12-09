using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC2021
{
    public class Day06 : DayBase, ITwoPartQuestion
    {
        public Day06()
        {
            Run(() => Part1(), () => Part2());
        }

        public string Part1()
        {
            var fishies = this.InputFile.Replace(",", "");
            var rules = new Dictionary<char, char>
            {
                ['8'] = '7', ['7'] = '6', ['6'] = '5', ['5'] = '4',
                ['4'] = '3', ['3'] = '2', ['2'] = '1', ['1'] = '0',
                ['0'] = '6'
            };

            for (int days = 0; days < 80; days++)
            {
                var sb = new StringBuilder();

                foreach (var fish in fishies)
                {
                    sb.Append(rules[fish]);
                    if (fish == '0')
                        sb.Append('8');
                }

                fishies = sb.ToString();
            }

            return $"{fishies.Length}";
        }

        public string Part2()
        {
            var fishTally = new Dictionary<int, long>
            {
                [0] = 0, [1] = 0, [2] = 0, [3] = 0,
                [4] = 0, [5] = 0, [6] = 0, [7] = 0,
                [8] = 0
            };

            foreach (var fish in this.InputFile.Split(','))
                fishTally[int.Parse(fish)]++;

            for (int days = 0; days < 256; days++)
            {
                var temp = fishTally[0];

                fishTally[0] = fishTally[1];
                fishTally[1] = fishTally[2];
                fishTally[2] = fishTally[3];
                fishTally[3] = fishTally[4];
                fishTally[4] = fishTally[5];
                fishTally[5] = fishTally[6];
                fishTally[6] = fishTally[7] + temp; //including rollover from 0 to 6
                fishTally[7] = fishTally[8];
                fishTally[8] = temp; //new fish
            }

            long answer = 0;
            for (int i = 0; i < 9; i++)
                answer += fishTally[i];

            return $"{answer}";
        }
    }
}