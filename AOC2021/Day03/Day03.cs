using System;
using System.Collections.Generic;

namespace AOC2021
{
    public class Day03 : DayBase, ITwoPartQuestion
    {
        private readonly List<string> input;

        public Day03()
        {
            input = this.InputFileAsStringList;
            Run(() => Part1(), () => Part2());
        }

        public string Part1()
        {
            var gamma = "";
            var epsilon = "";

            for (int i = 0; i < input[0].Length; i++)
            {
                var s = "";

                for (int j = 0; j < input.Count; j++)
                    s += input[j][i];

                var more1sThan0s = (s.Replace("0", "").Length > (input.Count / 2));

                gamma += more1sThan0s ? "1" : "0";
                epsilon += more1sThan0s ? "0" : "1";
            }

            var iGamma = Convert.ToInt64(gamma, 2);
            var iEpsilon = Convert.ToInt64(epsilon, 2);

            return $"{iGamma * iEpsilon}";
        }

        public string Part2()
        {
            var iGamma = RunPart2(searchFor: '1', diagnostics: input);
            var iEpsilon = RunPart2(searchFor: '0', diagnostics: input);

            return $"{iGamma * iEpsilon}";
        }

        public long RunPart2(char searchFor, List<string> diagnostics)
        {
            List<string> candidates;

            for (int i = 0; i < diagnostics[0].Length; i++)
            {
                var search = searchFor;
                var opposite = (search == '1') ? '0' : '1';
                var s = "";

                for (int j = 0; j < diagnostics.Count; j++)
                    s += diagnostics[j][i];

                var searchItemsFound = s.Replace(opposite.ToString(), "").Length;

                if (searchItemsFound != (diagnostics.Count - searchItemsFound))
                {
                    bool searchedForWon = (searchItemsFound > (diagnostics.Count / 2));
                    search = searchedForWon ? '1' : '0';
                }

                candidates = new List<string>();
                for (int j = 0; j < diagnostics.Count; j++)
                {
                    if (diagnostics[j][i] == search)
                        candidates.Add(diagnostics[j]);
                }

                diagnostics = candidates;

                if (diagnostics.Count == 1)
                    break;
            }

            return Convert.ToInt64(diagnostics[0], 2);
        }
    }
}