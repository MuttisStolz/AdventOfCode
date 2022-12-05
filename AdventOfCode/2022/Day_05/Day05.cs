namespace AdventOfCode
{
    public class Day05 : IPuzzle
    {
        Dictionary<int, LinkedList<char>> Cargo = new Dictionary<int, LinkedList<char>>();
        List<(int amount, int from, int to)> Movements = new List<(int amount, int from, int to)>();

        public void PuzzlePart1()
        {
            ReadInput();

            foreach(var movement in Movements)
            {
                this.MoveWithCrateMover9000(movement);
            }

            var res = GetTopsFromCargo();

            Console.WriteLine(res);
        }       

        public void PuzzlePart2()
        {
            ReadInput();

            foreach (var movement in Movements)
            {

                this.MoveWithCrateMover9001(movement);
            }

            var res = GetTopsFromCargo();

            Console.WriteLine(res);
        }

        private void MoveWithCrateMover9000((int amount, int from, int to) movement)
        {
            for(int i = 0; i< movement.amount; i++)
            {
                var item = this.Cargo[movement.from].First();
                this.Cargo[movement.from].RemoveFirst();
                this.Cargo[movement.to].AddFirst(item);
            }
        }

        private void MoveWithCrateMover9001((int amount, int from, int to) movement)
        {
            LinkedList<char> tmp = new LinkedList<char>();

            for (int i = 0; i < movement.amount; i++)
            {
                var item = this.Cargo[movement.from].First();
                this.Cargo[movement.from].RemoveFirst();
                tmp.AddFirst(item);
            }

            foreach(var item in tmp)
            {
                this.Cargo[movement.to].AddFirst(item);
            }
        }

        private string GetTopsFromCargo()
        {
            string res = "";

            for (int i = 1; i <= Cargo.Keys.Max(); i++ )
            {
                if(Cargo[i].Count > 0)
                    res += Cargo[i]?.First();
            }

            return res;
        }

        private void ReadInput()
        {
            //foreach (var line in File.ReadAllLines("2022/Day_05/test.txt"))
            foreach (var line in File.ReadAllLines("2022/Day_05/d05input.txt"))                
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
                                Cargo[j].AddLast(charArray[i]);
                            }
                            else
                            {
                                Cargo.Add(j, new LinkedList<char>());
                                Cargo[j].AddLast(charArray[i]);
                            }
                        }

                        j++;
                    }
                }
            }
        }
    }
}
