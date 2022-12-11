using AdventOfCode._2022.Day_11;
using CommonLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            var exInput = File.ReadAllLines(exampleFile).Where(s => !string.IsNullOrEmpty(s));
            var finalInput = File.ReadAllLines(inputFile).Where(s => !string.IsNullOrEmpty(s));

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

                    monkey.Items = block.ElementAt(1).Replace("Starting items:", "").Trim().Split(',').ToQueue<int,string>();

                    var operationInfos = block.ElementAt(2).Replace("Operation: new = ","").Trim().Split(' ');
                    monkey.SetOperationFunction(operationInfos[0], operationInfos[2], operationInfos[1]);

                    monkey.DivisibleBy = int.Parse(block.ElementAt(3).Replace("Test: divisible by ", "").Trim());

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

        private string PlayRounds(int rounds, List<Monkey> monkeyList)
        {
            string res = string.Empty;

            for (var round = 0; round < rounds; round++)
            {
                monkeyList.ForEach((Monkey m) => 
                { 
                    m.Inspect(); 
                });

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

        public void PuzzlePart2()
        {
            exampleMonkeys.ForEach((Monkey m) => { m.WorryFactor = 1; });
            Assert.Equal("2713310158", PlayRounds(10_000, exampleMonkeys));

            Console.WriteLine($"Solution Part 2:");
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
