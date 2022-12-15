using CommonLib.Extensions;
using System.Numerics;
using Xunit;

namespace AdventOfCode
{

    public class Day11 : IPuzzle
    {
        List<Monkey> monkeys;
        List<Monkey> exampleMonkeys;

        public Day11()
        {
            string exampleFile = "2022/Day_11/ex.txt";
            string inputFile = "2022/Day_11/input.txt";

            var exInput = File.ReadAllLines(exampleFile).RemoveNullOrEmptyEntries();
            var finalInput = File.ReadAllLines(inputFile).RemoveNullOrEmptyEntries();

            exampleMonkeys = CreateMonkeys(exInput.Count(m => m.StartsWith("Monkey")));
            monkeys = CreateMonkeys(finalInput.Count(m => m.StartsWith("Monkey")));

            var SetInputProperties = (IEnumerable<string> input, List<Monkey> monkeyList) =>
            {
                int currentMonkeyIndex = 0;

                while (input.Count() > 0)
                {
                    var block = input.Take(6);
                    input = input.Skip(6);

                    var monkey = monkeyList[currentMonkeyIndex];

                    var t = block.ElementAt(1).Replace("Starting items:", "").Trim().Split(',').ToList().Select(i => BigInteger.Parse(i)).ToList();
                    t.ForEach((e) => { monkey.Items.Enqueue(e); });

                    //monkey.Items = block.ElementAt(1).Replace("Starting items:", "").Trim().Split(',').ToQueue<BigInteger,string>();

                    var operationInfos = block.ElementAt(2).Replace("Operation: new = ","").Trim().Split(' ');
                    monkey.SetOperationFunction(operationInfos[0], operationInfos[2], operationInfos[1]);

                    monkey.DivisibleBy = BigInteger.Parse(block.ElementAt(3).Replace("Test: divisible by ", "").Trim());

                    var mti = int.Parse(block.ElementAt(4).Replace("If true: throw to monkey ", "").Trim());
                    monkey.TrueMonkey = monkeyList[mti];
                    var mfi = int.Parse(block.ElementAt(5).Replace("If false: throw to monkey ", "").Trim());
                    monkey.FalseMonkey = monkeyList[mfi];

                    //Console.WriteLine($"Setup Monkey {currentMonkeyIndex} with MT {mti} & MF {mfi} done");

                    currentMonkeyIndex++;
                }
            };

            SetInputProperties(exInput, exampleMonkeys);
            SetInputProperties(finalInput, monkeys);


        }


        public void PuzzlePart1()
        {
            Assert.Equal("10605", PlayRounds(20, exampleMonkeys));
            Console.WriteLine($"Solution Part 1: {PlayRounds(20, monkeys)}");            
        }

        public void PuzzlePart2()
        {
            BigInteger a = 1;

            exampleMonkeys.ForEach((Monkey m) => { a = a * m.DivisibleBy; });

            exampleMonkeys.ForEach((Monkey m) => { m.WorryFactor = 1; m.DivisibleBy = a; });
            Assert.Equal("2713310158", PlayRounds(10_000, exampleMonkeys));

            Console.WriteLine($"Solution Part 2:");
        }

        private string PlayRounds(int rounds, List<Monkey> monkeyList)
        {
            string res = string.Empty;

            for (var round = 0; round < rounds; round++)
            {
                monkeyList.ForEach((Monkey m) => 
                { 
                    m.Inspect(); 
                });

                Console.WriteLine($"After {round+1} Rounds:");
                //foreach (var m in monkeyList)
                //{

                //    Console.WriteLine($"\tMonkey {m.OwnIndex} inspected items :{m.InspectedCounter} times");
                //}

                //foreach (var monkey in monkeyList)
                //{
                //    //Console.WriteLine($"Monkey {monkey.OwnIndex}:");
                //    monkey.Inspect();
                //}

                //Console.WriteLine($"After Round {round+1}");
                //foreach (var m in monkeyList)
                //{
                //    Console.WriteLine($"\tMonkey {m.OwnIndex} holding:{m.Items.GetOutputString(',')}");
                //}

            }
            Console.WriteLine($"After {rounds} Rounds:");
            foreach (var m in monkeyList)
            {
                
                Console.WriteLine($"\tMonkey {m.OwnIndex} inspected items :{m.InspectedCounter} times");
            }

            return monkeyList
                .OrderByDescending(m => m.InspectedCounter)
                .Take(2)
                .Select(s=>s.InspectedCounter)
                .Product()
                .ToString();
        }

        

        private List<Monkey> CreateMonkeys(int amountOfMonkey)
        {
            List<Monkey> res = new List<Monkey>();

            for (int i = 0; i < amountOfMonkey; i++)
                res.Add(new Monkey(i));

            return res;

        }
    }
}
