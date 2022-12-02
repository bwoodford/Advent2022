using System;
using System.Linq;

namespace AdventOfCode2022.Problems.Day2
{
    internal class Code
    {
        public static int ProblemOne()
        {
            string[] lines = System.IO.File.ReadAllLines(@"./Problems/Day2/day2.txt");

            var score = 0;

            foreach(var line in lines)
            {
                var inputs = line.Split(' ');
                var oppPlay = Game.GetPlayType(inputs[0]);
                var myPlay = Game.GetPlayType(inputs[1]);
                var outcome = myPlay.Compare(oppPlay);

                score += (int)myPlay + (int)outcome;
            }

            return score;
        }

        public static int ProblemTwo() 
        {
            return 0;
        }
    }

    internal static class Game
    {
        public enum PlayType
        {
            Rock = 1,
            Paper = 2,
            Scissors = 3
        }

        public enum Outcome
        {
            Draw = 3,
            Win = 6,
            Loss = 0
        }

        public static PlayType GetPlayType(string input)
        {
            switch (input)
            {
                case "A":
                    return PlayType.Rock;
                case "B":
                    return PlayType.Paper;
                case "C":
                    return PlayType.Scissors;
                case "X":
                    return PlayType.Rock;
                case "Y":
                    return PlayType.Paper;
                case "Z":
                    return PlayType.Scissors;
                default:
                    throw new ArgumentException("play type not supported");
            }
        }

        public static Outcome Compare(this PlayType myType, PlayType oppType)
        {
            Outcome outcome;
            if (myType.CompareTo(oppType) == 0)
            {
                outcome = Outcome.Draw;
            }
            else if (myType == PlayType.Paper && oppType == PlayType.Rock)
            {
                outcome = Outcome.Win;
            }
            else if (myType == PlayType.Paper && oppType == PlayType.Scissors)
            {
                outcome = Outcome.Loss;
            }
            else if (myType == PlayType.Rock && oppType == PlayType.Scissors)
            {
                outcome = Outcome.Win;
            }
            else if (myType == PlayType.Rock && oppType == PlayType.Paper)
            {
                outcome = Outcome.Loss;
            }
            else if (myType == PlayType.Scissors && oppType == PlayType.Paper)
            {
                outcome = Outcome.Win;
            }
            else if (myType == PlayType.Scissors && oppType == PlayType.Rock)
            {
                outcome = Outcome.Loss;
            }
            else
            {
                throw new ArgumentException("cant compare play types");
            }
            return outcome;
        }
    }
}
