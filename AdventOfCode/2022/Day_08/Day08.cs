using CommonLib;
using CommonLib.Extensions;

namespace AdventOfCode
{
    public class Day08 : IPuzzle
    {
        TwoDimensionalArray<int> Cluster;
        Dictionary<(int r, int c), int> TreeScores = new Dictionary<(int r, int c), int>();
        int visibleTreeCounter = 0;

        public Day08()
        {
            this.ReadInput();
            //Console.WriteLine(this.Cluster);
        }

        public void PuzzlePart1()
        {
            Cluster.RunThroughAllValues(IsTreeVisible);
            Console.WriteLine($"Solution for Part1 = {visibleTreeCounter}");
        }

        public void PuzzlePart2()
        {
            Cluster.RunThroughAllValues(GetScore);
            var winner = this.TreeScores.MaxBy(t=>t.Value);
            Console.WriteLine($"Solution for Part2 = {winner.Key.r}|{winner.Key.c} = {winner.Value}");
        }

        public void IsTreeVisible((int r, int c) input)
        {
            if (Cluster.IsEdgeNode(input.r, input.c))
            {
                visibleTreeCounter++;
                return;
            }

            if (Cluster.GetLeftColumnValues(input.r, input.c)
                .Distinct().Any(a => a >= Cluster.Get(input.r, input.c)))
            {
                if (Cluster.GetRightColumnValues(input.r, input.c)
                .Distinct().Any(a => a >= Cluster.Get(input.r, input.c)))
                {
                    if (Cluster.GetUpperRowValues(input.r, input.c)
                        .Distinct().Any(a => a >= Cluster.Get(input.r, input.c)))
                    {
                        if (Cluster.GetButtonRowValues(input.r, input.c)
                            .Distinct().Any(a => a >= Cluster.Get(input.r, input.c)))
                        {
                            return;
                        }
                    }
                }
            }

            visibleTreeCounter++;
        }        

        public void GetScore((int r, int c) input)
        {
            List<int> scores = new List<int>();

            var currentHigh = Cluster.Get(input.r, input.c);

            scores.Add(CalcScore(currentHigh, Cluster.GetLeftColumnValues(input.r, input.c)));
            scores.Add(CalcScore(currentHigh, Cluster.GetRightColumnValues(input.r, input.c)));
            scores.Add(CalcScore(currentHigh, Cluster.GetUpperRowValues(input.r, input.c)));
            scores.Add(CalcScore(currentHigh, Cluster.GetButtonRowValues(input.r, input.c)));

            this.TreeScores.Add(input, scores.Product());
        }

        private int CalcScore(int current,List<int> values)
        {
            if (values.Count == 0)
                return 0;

            int score = 0;

            foreach(int value in values)
            {
                if(value >= current)
                {
                    score++;
                    break;
                }
                score++;
            }

            return score;
        }
        private void ReadInput()
        {
            var filename = "2022/Day_08/08input.txt"; //"2022/Day_08/example.txt";//
            var lines = File.ReadAllLines(filename);
            this.Cluster = new TwoDimensionalArray<int>(lines.Count(), lines.First().Length);


            foreach (var line in lines)
            {
                var columValues = line
                    .Select(i => int.Parse(i.ToString()))
                    .ToList();              

                this.Cluster.AddRow(columValues);
            }
        }
    }
}
