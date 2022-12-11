namespace AdventOfCode
{
    public class Day01 : IPuzzle
    {
        public void PuzzlePart1()
        {
            var elfs = this.ReadInput();

            var fatElf = elfs.MaxBy(t => t.sumCal);

            Console.WriteLine(fatElf);
        }

        public void PuzzlePart2()
        {
            var elfs = this.ReadInput();
            elfs = elfs.OrderByDescending(o => o.sumCal).ToList();

            var top3 = elfs[0].sumCal + elfs[1].sumCal + elfs[2].sumCal;

            Console.WriteLine(top3);
        }

        public List<(int elfNo, int sumCal)> ReadInput()
        {
            var res = new List<(int elfNo, int sumCal)>();

            int elfNo = 1;
            int sumCal = 0;

            foreach (var line in File.ReadAllLines("2022/Day_01/d01input.txt"))
            {
                if(line.Length > 0)
                {
                    sumCal += int.Parse(line);
                    continue;
                }

                res.Add((elfNo, sumCal));

                elfNo++;
                sumCal = 0;
            }

            return res;
        }
    }
}
