using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AOC2021
{
    public class Day08 : DayBase, ITwoPartQuestion
    {
        public string RuleString = "abcefg cf acdeg acdfg bcdf abdfg abdefg acf abcdefg abcdfg";
        public string[] Rules = null;

        public Day08()
        {
            Rules = RuleString.Split(" ");
            Run(() => Part1(), () => Part2());
        }

        public string Part1()
        {
            var counter = 0;
            foreach (var row in this.InputFileAsStringList)
            {
                var splitInput = row.Split(" | ");

                counter += CountOccurrences(splitInput[1], Rules[1]) +
                           CountOccurrences(splitInput[1], Rules[4]) +
                           CountOccurrences(splitInput[1], Rules[7]) +
                           CountOccurrences(splitInput[1], Rules[8]);
            }

            return $"{counter}";
        }

        private int CountOccurrences(string output, string rule)
        {
            var counter = 0;

            foreach (var digit in output.Split(" "))
            {
                if (digit.Length == rule.Length)
                    counter++;
            }
            return counter;
        }

        public string Part2()
        {
            var counter = 0;

            foreach (var row in this.InputFileAsStringList)
            {
                var splitInput = row.Split(" | ");
                counter += Resolve(splitInput[0], splitInput[1]);
            }

            return $"{counter}";
        }

        public int Resolve(string signals, string output)
        {
            /*
                digit   segments    pattern     unique?

                0       6           abc efg
                1       2             c  f      Y
                2       5           a cde g
                3       5           a cd fg
                4       4            bcd f      Y
                5       5           ab d fg
                6       6           ab defg
                7       3           a c  f      Y
                8       7           abcdefg     Y
                9       6           abcd fg

                                    8687497     occurrences in the 10 word signal
                                    6245317     order in which chars can be resolved
            */

            var mapping = new Dictionary<char, char>
            {
                [Occurrences(signals, 2, 9)] = 'f', // the 2 segment word 'cf'      has 'f', it occurs 9x in all the signals
                [Occurrences(signals, 4, 6)] = 'b', // the 4 segment word 'bcdf'    has 'b', it occurs 6x in all the signals
                [Occurrences(signals, 7, 4)] = 'e', // the 7 segment word 'abcdefg' has 'e', it occurs 4x in all the signals
                [' '] = ' '
            };

            AddMissingKey(ref mapping, signals, 2, 'c'); // in 2 segment 'cf'       , one missing key which must be 'c'
            AddMissingKey(ref mapping, signals, 4, 'd'); // in 4 segment 'bcdf'     , one missing key which must be 'd'
            AddMissingKey(ref mapping, signals, 3, 'a'); // in 3 segment 'acf'      , one missing key which must be 'a'
            AddMissingKey(ref mapping, signals, 7, 'g'); // in 7 segment 'abcdefg'  , one missing key which must be 'g'

            var translated = "";
            foreach (char c in output)
                translated += mapping[c];

            var digits = "";
            foreach (var signal in translated.Split(" "))
                for (int digit = 0; digit < Rules.Length; digit++)
                    if (String.Concat(signal.OrderBy(c => c)) == Rules[digit])
                        digits += digit.ToString();

            return int.Parse(digits);
        }

        private void AddMissingKey(ref Dictionary<char, char> mapping, string signals, int searchWordLength, char value)
        {
            var word = signals.Split(" ").Where(i => i.Length == searchWordLength).First();

            foreach (var c in word)
                if (!mapping.ContainsKey(c))
                {
                    mapping[c] = value;
                    break;
                }
        }

        private char Occurrences(string s, int searchWordLength, int rowOccurrences)
        {
            var word = s.Split(" ").Where(i => i.Length == searchWordLength).First();

            foreach (var c in word)
                if (s.Where(i => i == c).Count() == rowOccurrences)
                    return c;

            return ' ';
        }
    }
}