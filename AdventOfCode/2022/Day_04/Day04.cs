using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day04 : IPuzzle
    {
        public void PuzzlePart1()
        {
            var pairs = ReadElfPairs();
            var fullContained = pairs.Count(p => p.IsContainedByOneElf);

            Console.WriteLine($"Part1 Result is '{fullContained}' ");
        }

        public void PuzzlePart2()
        {
            var pairs = ReadElfPairs();
            var overlaps = pairs.Count(p => p.HasOverlap);

            Console.WriteLine($"Part1 Result is '{overlaps}' ");
        }

        private List<ElfPair> ReadElfPairs()
        {
            var res = new List <ElfPair>();

            foreach (var line in File.ReadAllLines("2022/Day_04/d04input.txt"))
            {
                var ranges = line.Split(',');
                var elfPair = new ElfPair(ranges[0], ranges[1]);
                res.Add(elfPair);
            }

            return res;
        }
    }

    public class ElfPair
    {
        public List<int> FirstElfAssignments { get; set; } = new List<int>();
        public List<int> SecondElfAssignments { get; set; } = new List<int>();

        public bool IsContainedByOneElf
        {
            get
            {
                return IsFullContained(FirstElfAssignments, SecondElfAssignments) 
                    || IsFullContained(SecondElfAssignments, FirstElfAssignments);
            }
        }

        public bool HasOverlap 
        {
            get
            {
                foreach (var entry in FirstElfAssignments)
                {
                    if (SecondElfAssignments.Contains(entry))
                        return true;
                }

                return false;
            }
        }

        public ElfPair(string range1, string range2)
        {
            FirstElfAssignments = FillAssignmentsArray(range1);
            SecondElfAssignments = FillAssignmentsArray(range2);
        }        

        private bool IsFullContained(List<int> a, List<int> b)
        {
            var res = true;

            foreach (var entry in a)
            {
                if(!b.Contains(entry))
                {
                    return false;
                }
            }

            return res;
        }

        private List<int> FillAssignmentsArray(string range)
        {
            var res = new List<int>();

            var boundaries = range.Split('-');
            var start = int.Parse(boundaries[0]);
            var end = int.Parse(boundaries[1]);

            for (int i = start; i<=end; i++)
            {
                res.Add(i);
            }


            return res;
        }
    }
}
