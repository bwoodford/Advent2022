using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2022.Problems.Day9 
{
    internal class Code
    {
        public static int ProblemOne()
        {
            var moves = GetMoves();
            var answer = new HashSet<Point>();
            var rope = new Rope().GetRope(2) ;

            answer.Add(rope.Tail);
            
            foreach ( var move in moves )
            {
                for (int i = 0; i < move.Value; i++)
                {
                    rope = rope.Move(move.Direction);
                    answer.Add(rope.Tail);
                }
            }

            return answer.Count;
        }

        public static int ProblemTwo()
        {
            var moves = GetMoves();
            var answer = new HashSet<Point>();
            var rope = new Rope().GetRope(10) ;

            answer.Add(rope.Tail);
            
            foreach ( var move in moves )
            {
                for (int i = 0; i < move.Value; i++)
                {
                    rope = rope.Move(move.Direction);
                    answer.Add(rope.Tail);
                }
            }

            return answer.Count;       
        }

        private struct Point
        {
            public int x { get; set; }
            public int y { get; set; }

            public Point Move(Direction dir)
            {
                switch (dir) {
                    case Direction.R:
                        return new Point { x = x + 1 , y = y };  
                    case Direction.L:
                        return new Point { x = x - 1 , y = y };  
                    case Direction.U:
                        return new Point { x = x, y = y + 1 };  
                    case Direction.D:
                        return new Point { x = x, y = y - 1 };
                    default:
                        return new Point { x = x , y = y };
                }
            }

            public Point Follow(Point leader)
            {
                var disX = leader.x - x;
                var disY = leader.y - y;

                // No change
                if (Math.Abs(disX) <= 1 && Math.Abs(disY) <= 1)
                {
                    return new Point { x = x, y = y };
                // Y has changed
                } else if (disX == 0 && Math.Abs(disY) > 1)
                {
                    return new Point { x = x, y = y + Math.Sign(disY) };
                // X has changed
                } else if (disY == 0 && Math.Abs(disX) > 1)
                {
                    return new Point { x = x + Math.Sign(disX), y = y };
                // Diagonal change
                } else
                {
                    return new Point { x = x + Math.Sign(disX), y = y + Math.Sign(disY) };
                }
            }
        }

        private struct Rope
        {
            public Point Tail => points.Last();
            public List<Point> points { get; set; }

            public Rope Move(Direction dir)
            {
                var ropeCopy = new List<Point>(points);
                ropeCopy[0] = ropeCopy[0].Move(dir);
                for (var i = 1; i < points.Count; i++)
                {
                    ropeCopy[i] = ropeCopy[i].Follow(ropeCopy[i-1]);
                }
                return new Rope { points = ropeCopy };
            }
            
            public Rope GetRope(int size)
            {
                return new Rope { points = new List<Point>(new Point[size]) };
            }
        }

        private static List<Move> GetMoves()
        {
            string[] lines = System.IO.File.ReadAllLines(@"./Problems/Day9/day9.txt");
            var moves = new List<Move>();

            foreach (var line in lines)
            {
                var input = line.Split(" ");
                moves.Add(new Move { Direction = (Direction)char.Parse(input[0]), Value = int.Parse(input[1]) });
            }
            
            return moves;
        }

        private struct Move 
        {
            public Direction Direction;
            public int Value;
        }

        private enum Direction
        {
            L = 'L',
            R = 'R',
            U = 'U',
            D = 'D'
        }
    }
}
