using CommonLib.Extensions;
using Xunit;

namespace AdventOfCode
{
    public class Day10 : IPuzzle
    {

        string exampleFile = "2022/Day_10/ex.txt";
        string inputFile = "2022/Day_10/input.txt";

        Queue<(string, int)> InputQueue = new Queue<(string, int)>();
        Queue<(string, int)> ExampleQueue = new Queue<(string, int)>();

        List<int> checkValuePoints = new List<int>() { 20, 60, 100, 140, 180, 220 };
        List<int> checkValuePoints2 = new List<int>() { 40, 80, 120, 160, 200, 240 };
        public Day10()
        {
            


        }

        private void ReadInput()
        {
            var stringToQueueItem = (string line) =>
            {
                if (line.StartsWith("noop"))
                {
                    return (line, 0);
                }

                var s = line.Split(' ');
                return (s[0], int.Parse(s[1]));

            };

            this.InputQueue = File.ReadAllLines(inputFile).ToQueue(stringToQueueItem);
            this.ExampleQueue = File.ReadAllLines(exampleFile).ToQueue(stringToQueueItem);
        }

        public void PuzzlePart1()
        {
            ReadInput();
            Assert.Equal("13140", GetSumPart1(this.ExampleQueue));
            Console.WriteLine($"Solution for Part1 =" + GetSumPart1(InputQueue));
        }

        public void PuzzlePart2()
        {
            this.ReadInput();

            var exRes = this.GetPicture2(ExampleQueue);

            Console.WriteLine($"Solution for Part2 =");

            foreach (var line in exRes)
            {
                Console.WriteLine(line);
            }

            Assert.Equal("##..##..##..##..##..##..##..##..##..##..", exRes[0]);
            Assert.Equal("###...###...###...###...###...###...###.", exRes[1]);
            Assert.Equal("####....####....####....####....####....", exRes[2]);
            Assert.Equal("#####.....#####.....#####.....#####.....", exRes[3]);
            Assert.Equal("######......######......######......####", exRes[4]);
            Assert.Equal("#######.......#######.......#######.....", exRes[5]);

            Console.WriteLine($"\n\nSolution for Part2 =\n\n");
            foreach (var line in this.GetPicture2(InputQueue))
            {
                Console.WriteLine(line);
            }
        }

        private ((string command, int addValue) com, int executeRound) 
            GetCommand(Queue<(string command, int addValue)> commandQueue, int currentRound)
        {
            var c = commandQueue.Dequeue();
            var e = 0;

            if (c.command.Equals("noop"))
            {
                e = currentRound + 0;
            }
            else
            {
                e = currentRound + 1;
            }

            return (c, e);
        }

       
        public List<string> GetPicture2(Queue<(string command, int addValue)> commandQueue)
        {
            List<string> res = new List<string>();            
            List<char> current = new List<char>();

            int x = 1;
            List<int> posi = new List<int>() { 1, 2, 3 };

            (string command, int addValue) currentCommand = ("",0);
            int cycleComplete = 0;

            bool isExcuteFinnish = true;

            var BeginnExecuting = (int i) => 
            {
                var tmp = this.GetCommand(commandQueue, i);
                currentCommand = tmp.com;
                cycleComplete = tmp.executeRound;

                isExcuteFinnish = false;

                //Console.WriteLine($"Start cycle   {i}: begin executing {currentCommand.command} {currentCommand.addValue}");
            };

            var FinnishExecuting = () =>
            {
                if (!currentCommand.command.Equals("noop"))
                {
                    x += currentCommand.addValue;

                    posi.Clear();
                    posi.Add(x +2);
                    posi.Add(x + 1);
                    posi.Add(x);
                }

                isExcuteFinnish = true;

                //Console.WriteLine($"finish executing addx {currentCommand.command} {currentCommand.addValue} (Register X is now {x})");
            };


            for (int j = 1; j <= 6; j++)
            {
                x = 1;
                cycleComplete = 1;

                for (int i = 1; i <= 40; i++)
                {
                    if(isExcuteFinnish)
                        BeginnExecuting(i);

                    var c = posi.Contains(i) ? '#' : '.';
                    current.Add(c);
                    //Console.WriteLine(new string(current.ToArray()));

                    if (i == cycleComplete)
                    {
                        FinnishExecuting();
                    }
                }

                //Console.WriteLine("Line Read = " + new string(current.ToArray()));
                res.Add(new string(current.ToArray()));
                current.Clear();
            }

            return res;
        }

        public string GetSumPart1(Queue<(string command, int addValue)> commandQueue)
        {
            int x = 1;
            int nextdeque = 1;            
            List<int> signalStrengths = new List<int>();

            var maxCycles = commandQueue.Count(c => c.Item1.Equals("noop")) + (commandQueue.Count(c => !c.Item1.Equals("noop")) * 2);

            (string command, int addValue) currentCommand = ("", 0);

            for (int i = 1; i<= maxCycles; i++)
            {
                if(i== nextdeque)
                {
                    if(i != 1 && !currentCommand.command.Equals("noop"))
                    {
                        x += currentCommand.addValue;
                    }

                    currentCommand = commandQueue.Dequeue();

                    if(currentCommand.command.Equals("noop"))
                    {
                        nextdeque = nextdeque+1;
                    }
                    else
                    {
                        nextdeque = nextdeque + 2;
                    }
                }


                if(checkValuePoints.Contains(i))
                {
                    var signalStrength = i * x;
                    signalStrengths.Add(signalStrength);
                }
            }

            return signalStrengths.Sum().ToString();
        }
        
    }
}
