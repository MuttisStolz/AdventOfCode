using System.Numerics;

namespace AdventOfCode
{
    public class Monkey
    {
        public Monkey TrueMonkey { get; set; }
        public Monkey FalseMonkey { get; set; }
        public Queue<BigInteger> Items { get; set; } = new Queue<BigInteger>();
        public BigInteger DivisibleBy { get; set; }

        public BigInteger WorryFactor { get; set; } = 3;

        public int OwnIndex { get; private set; }
        public int InspectedCounter { get; private set; } =0;

        private  Func<BigInteger> operationFunc;
        private BigInteger old;
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
                "*" => () => { return GetOperationValue(l) * GetOperationValue(r); },
                "+" => () => { return GetOperationValue(l) + GetOperationValue(r); },
                _ => throw new Exception($" Unknown Operation '{o}'")
            };
        }
        

        public BigInteger Operation()
        {
            return this.operationFunc();
        }

        private BigInteger GetOperationValue(string s)
        {
            if(s.Equals("old"))
            {
                return old;
            }

            return BigInteger.Parse(s);
        }

        public void Inspect()
        {
            while (this.Items.Count > 0)
            {
                InspectedCounter++;

                var level = this.Items.Dequeue();
                this.old = level;

                var worry = this.operationFunc();
                var worry3 = worry / WorryFactor;

                bool isDivisibleBy = worry3 % this.DivisibleBy == 0;

                var targetMonkey = isDivisibleBy ? TrueMonkey : FalseMonkey;
                targetMonkey.Items.Enqueue(worry3);


                //Console.WriteLine($"\tMonkey inspects an item with a worry level of {level}.");
                //Console.WriteLine($"\t\tWorry level is calculate to {worry}.");
                //Console.WriteLine($"\t\tMonkey gets bored with item. Worry level is divided by {WorryFactor} to {worry3}.");
                //Console.WriteLine($"\t\tCurrent worry level" + (isDivisibleBy ? "is " : "is not ") + $" divisible by {DivisibleBy}.");
                //Console.WriteLine($"\t\tItem with worry level {worry3} is thrown to monkey {targetMonkey.OwnIndex}.");
            }
        }        
    }
}
