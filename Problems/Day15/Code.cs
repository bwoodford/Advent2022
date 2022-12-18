
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problems.Day15
{
    internal class Code
    {
        public static int ProblemOne()
        {
            var map = new Map();

            var points = map.Points.Where(x => x.Y == 10);
            var beacons = points.Where(x => x.PointType == PointType.Beacon).Count();
            return points.Count() - (beacons * 2);
        }

        public static int ProblemTwo()
        {
            var map = new Map();
            return 0;
        }
    }

    internal struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public PointType PointType { get; set; }

        public Point(int x, int y, PointType pointType)
        {
            X = x;
            Y = y;
            PointType = pointType;
        }

        public int Distance(Point other)
        {
            return Math.Abs(this.X - other.X) + Math.Abs(this.Y - other.Y);
        }
    }

    internal class Map
    {
        public HashSet<Point> Points { get; set; }

        public Map()
        {
            var lines = System.IO.File.ReadAllLines(@"./Problems/Day15/day15.txt");
            Points = new HashSet<Point>();
            var regex = new Regex(@"\d+");
            foreach(var line in lines)
            {
                var m = regex.Matches(line);
                var sensor = new Point(int.Parse(m[0].Value), int.Parse(m[1].Value), PointType.Sensor);
                var beacon = new Point(int.Parse(m[2].Value), int.Parse(m[3].Value), PointType.Beacon);

                Points.Add(sensor);
                Points.Add(beacon);

                this.Fill(sensor, sensor.Distance(beacon));
            }
        }

        public void Fill(Point p, int distance)
        {
            var i = 0;
            while(p.Distance(new Point(p.X, p.Y + i, PointType.Filled)) <= distance)
            {
                var j = 0;
                while(p.Distance(new Point(p.X + j, p.Y + i, PointType.Filled)) <= distance)
                {
                    this.Points.Add(new Point(p.X + j, p.Y + i, PointType.Filled));
                    this.Points.Add(new Point(p.X - j, p.Y - i, PointType.Filled));
                    this.Points.Add(new Point(p.X + j, p.Y - i, PointType.Filled));
                    this.Points.Add(new Point(p.X - j, p.Y + i, PointType.Filled));
                    j++;
                }
                i++;
            }
        }
    }

    internal enum PointType
    {
        Beacon,
        Sensor,
        Filled,
        Empty
    }
}
