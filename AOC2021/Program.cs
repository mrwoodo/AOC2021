﻿using System;
using System.Linq;
using System.Reflection;

namespace AOC2021
{
    class Program
    {
        static void Main()
        {
            var days = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(DayBase)));

            //foreach (var day in days)
            //    Activator.CreateInstance(day);

            _ = new Day08();

            //Console.ReadLine();
        }
    }
}