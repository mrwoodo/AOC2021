using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2021
{
    public class Day05 : DayBase, ITwoPartQuestion
    {
        public readonly Lines lines = null;

        public Day05()
        {
            lines = new Lines().Parse(this.InputFileAsStringList);

            Run(() => Part1(), () => Part2());
        }

        public string Part1()
        {
            return @$"{lines
                .ConstructMap(simpleMode: true)
                .Cast<int>()
                .Where(i => i > 1)
                .Count()}";
        }

        public string Part2()
        {
            return @$"{lines
                .ConstructMap(simpleMode: false)
                .Cast<int>()
                .Where(i => i > 1)
                .Count()}";
        }
    }

    public class Lines : List<Line>
    {
        private readonly Point Max = new Point();

        public Lines Parse(List<string> s)
        {
            foreach (var i in s)
                this.Add(new Line(i.Split(" -> ")));

            return this;
        }

        public int[,] ConstructMap(bool simpleMode = true)
        {
            var array = new int[this.Max.x + 1, this.Max.y + 1];

            foreach (var l in this)
            {
                var dx = (l.from.x < l.to.x) ? 1 : -1;
                var dy = (l.from.y < l.to.y) ? 1 : -1;

                if (l.Horizontal)
                    dy = 0;

                if (l.Vertical)
                    dx = 0;

                var x = l.from.x;
                var y = l.from.y;
                var finished = simpleMode && Math.Abs(dx) == 1 && Math.Abs(dy) == 1; //ignore diagonals in Part 1

                while (!finished)
                {
                    array[x, y]++;
                    finished = x == l.to.x && y == l.to.y;
                    x += dx;
                    y += dy;
                }
            }

            return array;
        }

        public new void Add(Line l)
        {
            if (l.from.x > Max.x)
                Max.x = l.from.x;
            if (l.from.y > Max.y)
                Max.y = l.from.y;
            if (l.to.x > Max.x)
                Max.x = l.to.x;
            if (l.to.y > Max.y)
                Max.y = l.to.y;

            base.Add(l);
        }
    }

    public class Line
    {
        public Point from;
        public Point to;

        public Line(string [] coords)
        {
            from = new Point(coords[0].Split(',')[0], coords[0].Split(',')[1]);
            to = new Point(coords[1].Split(',')[0], coords[1].Split(',')[1]);
        }

        public bool Horizontal => (this.from.y == this.to.y);
        public bool Vertical => (this.from.x == this.to.x);
        public int MaxX => Math.Max(this.from.x, this.to.x);
        public int MaxY => Math.Max(this.from.y, this.to.y);
        public int MinX => Math.Min(this.from.x, this.to.x);
        public int MinY => Math.Min(this.from.y, this.to.y);
    }

    public class Point
    {
        public int x;
        public int y;

        public Point()
        {
            x = 0;
            y = 0;
        }

        public Point(string X, string Y)
        {
            x = int.Parse(X);
            y = int.Parse(Y);
        }
    }
}