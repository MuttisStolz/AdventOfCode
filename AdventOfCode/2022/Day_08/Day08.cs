using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day08 : IPuzzle
    {
        TwoDimensionalArray<int> Cluster;

        int visibleTreeCounter = 0;

        public Day08()
        {
            this.ReadInput();
            Console.WriteLine(this.Cluster);
        }

        public void PuzzlePart1()
        {
            Cluster.RunThroughAllValues(IsTreeVisible);
            Console.WriteLine($"Solution for Part1 = {visibleTreeCounter}");
        }

        public void IsTreeVisible((int r, int c) input)
        {
            Console.WriteLine($"Check if Tree is Visible for Coardinate {input.r}|{input.c} " 
                + $"with Value {Cluster.Get(input.r, input.c)}");

            if (Cluster.IsEdgeNode(input.r, input.c))
            {
                visibleTreeCounter++;
                return;
            }

            if (Cluster.GetLeftColumnValues(input.r, input.c)
                .Distinct()
                .Any(a => a >= Cluster.Get(input.r, input.c)))
            {
                if (Cluster.GetRightColumnValues(input.r, input.c)
                .Distinct()
                .Any(a => a >= Cluster.Get(input.r, input.c)))
                {
                    if (Cluster.GetUpperRowValues(input.r, input.c)
                        .Distinct()
                        .Any(a => a >= Cluster.Get(input.r, input.c)))
                    {
                        if (Cluster.GetButtonRowValues(input.r, input.c)
                            .Distinct()
                            .Any(a => a >= Cluster.Get(input.r, input.c)))
                        {
                            return;
                        }
                    }
                }
            }

            visibleTreeCounter++;
        }        

        public void PuzzlePart2()
        {
            Console.WriteLine($"Solution for Part2 = ");
        }

        private void ReadInput()
        {
            var filename = "2022/Day_08/08input.txt";//"2022/Day_08/example.txt"; //
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
