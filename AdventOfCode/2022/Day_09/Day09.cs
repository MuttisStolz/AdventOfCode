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
        CoordinatePoint Tail = new CoordinatePoint(0, 0);


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
                    Tail.MoveTo(Head);

                    Console.WriteLine($"Head = {Head} | ");
                    Console.WriteLine($"Tail = {Tail} | {Tail.VisitPoints.Count}");
                }
            }

            Console.WriteLine($"Solution for Part1 = {this.Tail.VisitPoints.Count}");
        }

        public void PuzzlePart2()
        {
            Console.WriteLine($"Solution for Part2 = {Input.Count}");
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

        public bool IsCloseTo(CoordinatePoint other)
        {
            var xdiff = Math.Abs(this.X - other.X);
            var ydiff = Math.Abs(this.Y - other.Y);

            return xdiff == 1 || ydiff == 1;
        }

        public override string ToString()
        {
            return $"({this.X}|{this.Y})";
        }
    }

}
