using System.Collections.Generic;
using System.Linq;

namespace AOC2021
{
    public class Day04 : DayBase, ITwoPartQuestion
    {
        public static readonly int ColRowSize = 5;
        public List<int> draw = new List<int>();
        public Tiles tiles = new Tiles(ColRowSize);
        public int numberOfBoards = 0;

        public Day04()
        {
            var data = this.InputFileAsStringList;
            var iRow = 0;
            var iCol = 0;

            for (var i = 0; i < data.Count; i++)
            {
                if (i == 0)
                {
                    foreach (var num in data[i].Split(','))
                        draw.Add(int.Parse(num));
                }
                else
                {
                    if (data[i].Length == 0)
                    {
                        numberOfBoards++;
                        iRow = iCol = 0;
                    }
                    else
                    {
                        foreach (var num in data[i].Split(' '))
                        {
                            if (num.Trim().Length > 0)
                            {
                                tiles.Add(new Tile
                                {
                                    board = numberOfBoards - 1,
                                    row = iRow,
                                    col = iCol,
                                    value = int.Parse(num)
                                });

                                iCol++;
                                if (iCol == ColRowSize)
                                {
                                    iCol = 0;
                                    iRow++;
                                }
                            }
                        }
                    }
                }
            }

            Run(() => Part1(), () => Part2());
        }

        public string Part1()
        {
            foreach (var num in draw)
               tiles.DrawNumber(num);

            return $"{tiles.winningScoreHistory.First()}";
        }

        public string Part2()
        {
            return $"{tiles.winningScoreHistory.Last()}";
        }
    }

    public class Tiles : List<Tile>
    {
        private readonly int colsPerRow;
        public readonly List<int> winningBoardHistory = new List<int>();
        public List<int> winningScoreHistory = new List<int>();

        public Tiles(int ColsPerRow)
        {
            colsPerRow = ColsPerRow;
        }

        public void DrawNumber(int num)
        {
            foreach (var t in this.Where(i => i.value == num))
            {
                t.ticked = true;
                var boardResult = this.Where(i => i.board == t.board &&
                                        i.ticked &&
                                        !winningBoardHistory.Contains(t.board));

                if (boardResult.GroupBy(i => i.row).Where(i => i.Count() == colsPerRow).Any() ||
                    boardResult.GroupBy(i => i.col).Where(i => i.Count() == colsPerRow).Any())
                {
                    winningBoardHistory.Add(t.board);
                    winningScoreHistory.Add(t.value * this.Where(i => i.board == t.board && !i.ticked).Sum(i => i.value));
                }
            }
        }
    }

    public class Tile
    {
        public int row = 0;
        public int col = 0;
        public int board = 0;
        public int value = -1;
        public bool ticked = false;
    }
}