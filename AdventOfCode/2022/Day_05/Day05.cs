using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day05 : IPuzzle
    {
        Dictionary<int, Queue<char>> Cargo = new Dictionary<int, Queue<char>>();
        List<(int amount, int from, int to)> Movements = new List<(int amount, int from, int to)>();

        public void PuzzlePart1()
        {
            ReadInput();

            foreach(var movement in Movements)
            {
                var tmp = GetTopsFromCargo();
                Console.WriteLine(tmp);
                this.Move(movement);
                tmp = GetTopsFromCargo();
                Console.WriteLine(tmp);
            }

            var res = GetTopsFromCargo();

            Console.WriteLine(res);
        }       

        public void PuzzlePart2()
        {
            throw new NotImplementedException();
        }

        private void Move((int amount, int from, int to) movement)
        {
            for(int i = 0; i< movement.amount; i++)
            {
                throw new NotImplementedException("No time to finnish it");
                var item = this.Cargo[movement.from].Dequeue();
                var tmp = this.Cargo[movement.to];
                //this.Cargo[movement.to].Clear();
                //this.Cargo[movement.from].Enqueue(item);
            }
        }

        private string GetTopsFromCargo()
        {
            string res = "";

            for (int i = 1; i <= Cargo.Keys.Max(); i++ )
            {
                res += Cargo[i]?.First();
            }

            return res;
        }

        private void ReadInput()
        {
            foreach (var line in File.ReadAllLines("2022/Day_05/test.txt"))
            {
                var charArray = line.ToArray<char>();

                if (line.StartsWith("move"))
                {
                    var s = line.Split(' ');
                    int amount = int.Parse(s[1]);
                    int from = int.Parse(s[3]);
                    int to = int.Parse(s[5]);

                    Movements.Add((amount, from, to));
                }
                else if(line.Length > 0 && !char.IsDigit(line.First()))
                {
                    int j = 1;

                    for (int i = 1; i < line.Length; i = i + 4)
                    {
                        if (char.IsLetter(charArray[i]))
                        {
                            if(Cargo.ContainsKey(j))
                            {
                                Cargo[j].Enqueue(charArray[i]);
                            }
                            else
                            {
                                Cargo.Add(j, new Queue<char>());
                                Cargo[j].Enqueue(charArray[i]);
                            }
                        }

                        j++;
                    }
                }
            }
        }
    }
}
