using AdventOfCode;
using CommonLib.Enums;

public class Day02 : IPuzzle
{
    public Day02()
    {
    }

    public void PuzzlePart1()
    {
        int score = 0;
        var rounds = ReadInput();

        foreach (var round in rounds)
        {
            score += round.me.GetSignScore() + round.me.Versus(round.other);
        }

        Console.WriteLine($"Score Total is {score}");
    }

    public void PuzzlePart2()
    {
        int score = 0;
        var rounds = ReadInput();

        foreach (var round in rounds)
        {
            var newMe = (round.me) switch
            {
                Handsign.Rock => round.other.GetLoser(),
                Handsign.Paper => round.other,
                Handsign.Scissor => round.other.GetWinner(),
            };

            score += newMe.GetSignScore() + newMe.Versus(round.other);
        }

        Console.WriteLine($"Score Total is {score}");
    }

    private List<(Handsign other, Handsign me)> ReadInput()
    {
        var res = new List<(Handsign other, Handsign me)>();
        foreach (var line in File.ReadAllLines("2022/Day_02/d02input.txt"))
        {
            var r = line.Split(' ');
            res.Add((ConvertToHandsigns(r[0]), ConvertToHandsigns(r[1])));
        }

        return res;
    }

    private Handsign ConvertToHandsigns(string c) => c switch
    {
        "A" or "X" => Handsign.Rock,
        "B" or "Y" => Handsign.Paper,
        "C" or "Z" => Handsign.Scissor,
        _ => throw new NotSupportedException()
    };

   
}