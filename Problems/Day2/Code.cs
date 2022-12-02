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
                var oppPlay = GameOne.GetPlayType(inputs[0]);
                var myPlay = GameOne.GetPlayType(inputs[1]);
                var outcome = myPlay.CalculateOutcome(oppPlay);

                score += (int)myPlay + (int)outcome;
            }

            return score;
        }

        public static int ProblemTwo() 
        {
            string[] lines = System.IO.File.ReadAllLines(@"./Problems/Day2/day2.txt");

            var score = 0;

            foreach(var line in lines)
            {
                var inputs = line.Split(' ');
                var oppPlay = GameTwo.GetPlayType(inputs[0]);
                var outcome = GameTwo.GetOutcome(inputs[1]);
                var myPlay = GameTwo.CalculatePlay(oppPlay, outcome);

                score += (int)myPlay + (int)outcome;
            }

            return score;
        }
    }

    internal static class GameOne
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

        public static Outcome CalculateOutcome(this PlayType myType, PlayType oppType)
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


    internal static class GameTwo
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
                default:
                    throw new ArgumentException("play type not supported");
            }
        }

        public static Outcome GetOutcome(string input)
        {
            switch (input)
            {
                case "X":
                    return Outcome.Loss;
                case "Y":
                    return Outcome.Draw;
                case "Z":
                    return Outcome.Win;
                default:
                    throw new ArgumentException("outcome not supported");
            }
        }

        public static PlayType CalculatePlay(PlayType oppPlay, Outcome outcome)
        {
            PlayType myPlay;

            if (oppPlay == PlayType.Paper)
            {
                myPlay = PlayType.Paper;

                if (outcome == Outcome.Loss)
                {
                    myPlay = PlayType.Rock;
                }
                else if (outcome == Outcome.Win)
                {
                    myPlay = PlayType.Scissors;
                }
            }
            else if (oppPlay == PlayType.Scissors)
            {
                myPlay = PlayType.Scissors;

                if (outcome == Outcome.Loss)
                {
                    myPlay = PlayType.Paper;
                }
                else if (outcome == Outcome.Win)
                {
                    myPlay = PlayType.Rock;
                }
            }
            else if (oppPlay == PlayType.Rock)
            {
                myPlay = PlayType.Rock;

                if (outcome == Outcome.Loss)
                {
                    myPlay = PlayType.Scissors;
                }
                else if (outcome == Outcome.Win)
                {
                    myPlay = PlayType.Paper;
                }
            }
            else
            {
                throw new ArgumentException("play type and outcome not supported");
            }
            return myPlay;
        }
    }
}
