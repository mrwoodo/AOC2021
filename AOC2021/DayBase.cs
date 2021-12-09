﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AOC2021
{
    public class DayBase
    {
        public DayBase()
        {
            Console.WriteLine("".PadLeft(80, '-'));
            Console.WriteLine($"{this.GetType().Name}\r\n");
        }

        public string InputFile => File.ReadAllText($"{this.GetType().Name}\\{this.GetType().Name}.txt");
        public List<string> InputFileAsStringList => InputFile.Split("\r\n").ToList();
        public List<int> InputFileAsIntList => (from line in InputFileAsStringList select int.Parse(line)).ToList();
        public List<long> InputFileAsLongList => (from line in InputFileAsStringList select long.Parse(line)).ToList();

        public void Run(Func<string> part1, Func<string> part2)
        {
            var s = new Stopwatch();

            s.Start();
            var part1Answer = part1();
            s.Stop();
            Console.WriteLine($"Part 1....{part1Answer} (took {s.ElapsedMilliseconds}ms)");

            s.Reset();
            s.Start();
            var part2Answer = part2();
            s.Stop();
            Console.WriteLine($"Part 2....{part2Answer} (took {s.ElapsedMilliseconds}ms)");

            if ((part1Answer.Length == 0) || (part2Answer.Length == 0))
                Console.WriteLine("!! IMCOMPLETE !!");
        }
    }
}