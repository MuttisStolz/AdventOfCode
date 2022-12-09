using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace AdventOfCode
{
    public class Day09 : IPuzzle
    {
        List<(char direction, int amountOfSteps)> Input = new List<(char, int)>();

        CoordinatePoint Head = new CoordinatePoint(0, 0);
        CoordinatePoint Tail1 = new CoordinatePoint(0, 0);

        List<CoordinatePoint> Tails = new List<CoordinatePoint>();

        public Day09()
        {
            this.ReadInput();
        }


        public void PuzzlePart1()
        {
            foreach (var command in Input)
            {
                Console.WriteLine($" === {command.direction} {command.amountOfSteps} ===");
                for (int step = 0; step < command.amountOfSteps; step++)
                {
                    Head.MoveStep(command.direction);
                    Tail1.MoveTo(Head);

                    Console.WriteLine($"Head = {Head} ");
                    Console.WriteLine($"Tail = {Tail1} ");
                }
            }

            Console.WriteLine($"Solution for Part1 = {Tail1}");
        }

        private void InitTails(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Tails.Add(new CoordinatePoint());
            }
        }
        public void PuzzlePart2()
        {
            this.InitTails(10);

            foreach (var command in Input)
            {
                Console.WriteLine($" === {command.direction} {command.amountOfSteps} ===");
                for (int step = 0; step < command.amountOfSteps; step++)
                {
                    Head.MoveStep(command.direction);
                    Console.WriteLine($"Head = {Head} ");

                    Tails[0].MoveTo(Head);
                    Console.WriteLine($"Tail{1} = {Tails[0]} ");

                    for (int i = 1; i<Tails.Count-1;i++)
                    {
                        Tails[i].MoveTo(Tails[i - 1]);
                        Console.WriteLine($"Tail{i+1} = {Tails[i]} ");
                    }                    
                }
            }

        }

        private void ReadInput()
        {
            var filename = "2022/Day_09/input.txt"; //"2022/Day_08/example.txt";//
            var lines = File.ReadAllLines(filename);

            foreach (var line in lines)
            {
                var s = line.Split(' ');
                this.Input.Add((char.Parse(s[0]), int.Parse(s[1])));
            }            
        }
    }

    public class CoordinatePoint
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public List<(int, int)> VisitPoints { get; private set; } = new List<(int, int)>();

        public CoordinatePoint()
        {
            this.X = 0;
            this.Y = 0;
            SaveVisitPoint();
        }

        public CoordinatePoint(int x, int y)
        {
            this.X = x;
            this.Y = y;
            SaveVisitPoint();
        }

        public void MoveTo(CoordinatePoint other)
        {
            var xDiff = this.GetXDiff(other);
            var yDiff = this.GetYDiff(other);

            if (this.Y == other.Y && xDiff  > 1)
            {
                var direction = this.X > other.X ? 'L' : 'R';
                this.MoveStep(direction);
                SaveVisitPoint();
                
            }
            else if(this.X == other.X && yDiff > 1)
            {
                var direction = this.Y > other.Y ? 'D' : 'U';
                this.MoveStep(direction);
                SaveVisitPoint();
            }
            else if(xDiff == 2 || yDiff == 2)
            {
                var direction = this.X > other.X ? 'L' : 'R';
                this.MoveStep(direction);
                var direction2 = this.Y > other.Y ? 'D' : 'U';
                this.MoveStep(direction2);
                SaveVisitPoint();
            }

        }

        private void SaveVisitPoint()
        {
            if (VisitPoints.Count(v=>v.Item1 == X && v.Item2 ==Y) == 0)
                VisitPoints.Add((X, Y));
        }

        public int GetXDiff(CoordinatePoint other)
        {
            return Math.Abs(this.X - other.X);
        }

        public int GetYDiff(CoordinatePoint other)
        {
            return Math.Abs(this.Y - other.Y);
        }

        public void MoveStep(char direction)
        {
            switch (direction)
            {
                case 'U':
                    this.Y++;
                    break;
                case 'D':
                    this.Y--;
                    break;
                case 'L':
                    this.X--;
                    break;
                case 'R':
                    this.X++;
                    break;
                default:
                    throw new Exception($"Direction '{direction}' unknown");
            }
            
        }

        public override string ToString()
        {
            return $"Current Position ({this.X}|{this.Y}). Visit Points Count = {this.VisitPoints.Count}";
        }
    }

}
