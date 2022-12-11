using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Day02;

namespace AdventOfCode._2022.Day_11
{
    public class Monkey
    {
        public Monkey TrueMonkey { get; set; }
        public Monkey FalseMonkey { get; set; }
        public Queue<int> Items { get; set; } = new Queue<int>();
        public int DivisibleBy { get; set; }


        public int OwnIndex { get; private set; }
        public int InspectedCounter { get; private set; } =0;

        private  Func<int> operationFunc;
        private int old;
        private string l;
        private string r;
        private string o;
        
        public Monkey(int index)
        {
            this.OwnIndex = index;
        }

        public void SetOperationFunction(string l, string r, string o)
        {
            this.l = l;
            this.r = r;
            this.o = o;

            operationFunc = o switch
            {
                "*" => () => { return GetOperationValue(l) * GetOperationValue(r); }
                ,
                "+" => () => { return GetOperationValue(l) + GetOperationValue(r); }
                ,
                _ => throw new Exception($" Unknown Operation '{o}'")
            };
        }
        

        public int Operation()
        {
            return this.operationFunc();
        }

        private int GetOperationValue(string s)
        {
            if(s.Equals("old"))
            {
                return old;
            }

            return int.Parse(s);
        }

        public void Inspect()
        {
            while (this.Items.Count > 0)
            {
                InspectedCounter++;

                var level = this.Items.Dequeue();
                this.old = level;

                var worry = this.operationFunc();
                var worry3 = worry / 3;

                bool isDivisibleBy = worry3 % this.DivisibleBy == 0;

                var targetMonkey = isDivisibleBy ? TrueMonkey : FalseMonkey;
                targetMonkey.Items.Enqueue(worry3);


                //Console.WriteLine($"\tMonkey inspects an item with a worry level of {level}.");
                //Console.WriteLine($"\t\tWorry level is calculate to {worry}.");
                //Console.WriteLine($"\t\tMonkey gets bored with item. Worry level is divided by 3 to {worry3}.");
                //Console.WriteLine($"\t\tCurrent worry level" + (isDivisibleBy ? "is " : "is not ") + $" divisible by {DivisibleBy}.");
                //Console.WriteLine($"\t\tItem with worry level {worry3} is thrown to monkey {targetMonkey.OwnIndex}.");
            }
        }        
    }
}
