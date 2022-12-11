using CommonLib.Enums;
using static Day02;

namespace AdventOfCode
{
    public static class HandsignExtension
    {
        public const int WIN = 6;
        public const int DRAW = 3;
        public const int LOSE = 0;
        public static int Versus(this Handsign me, Handsign other)
        {
            var res = (me, other) switch
            {
                (Handsign.Rock, Handsign.Rock) => DRAW,
                (Handsign.Rock, Handsign.Scissor) => WIN,
                (Handsign.Rock, Handsign.Paper) => LOSE,

                (Handsign.Scissor, Handsign.Scissor) => DRAW,
                (Handsign.Scissor, Handsign.Rock) => LOSE,
                (Handsign.Scissor, Handsign.Paper) => WIN,

                (Handsign.Paper, Handsign.Paper) => DRAW,
                (Handsign.Paper, Handsign.Rock) => WIN,
                (Handsign.Paper, Handsign.Scissor) => LOSE,
                _ => throw new NotImplementedException(),
            };

            return res;
        }
        public static int GetSignScore(this Handsign me)
        {
            return (int)me;
        }
        public static Handsign GetWinner(this Handsign other)
        {
            var res = (other) switch
            {
                Handsign.Rock => Handsign.Paper,
                Handsign.Paper => Handsign.Scissor,
                Handsign.Scissor => Handsign.Rock,
                _ => throw new NotImplementedException(),
            };

            return res;
        }
        public static Handsign GetLoser(this Handsign other)
        {
            var res = (other) switch
            {
                Handsign.Rock => Handsign.Scissor,
                Handsign.Paper => Handsign.Rock,
                Handsign.Scissor => Handsign.Paper,
                _ => throw new NotImplementedException(),
            };

            return res;
        }
    }
}
