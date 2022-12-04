using System;
using System.Collections.Generic;

namespace AdventOfCode2022.Problems.Day3
{
    internal class Code
    {
        public static int ProblemOne()
        {
            string[] lines = System.IO.File.ReadAllLines(@"./Problems/Day3/day3.txt");

            var priority = 0;

            foreach(var line in lines)
            {
                var mid = line.Length / 2;
                var sack1 = line.Substring(0, mid);
                var sack2 = line.Substring(mid);

                var dups = new HashSet<char>();

                foreach(var c in sack1.ToCharArray())
                {
                    if (sack2.Contains(c))
                    {
                        dups.Add(c);
                    }
                }

                foreach(var d in dups)
                {
                    priority += (int)Enum.Parse(typeof(Priority), d.ToString());
                }
            }
            return priority;
        }

        public static int ProblemTwo() 
        {
            string[] lines = System.IO.File.ReadAllLines(@"./Problems/Day3/day3.txt");

            var priority = 0;

            for(var i = 0; i < lines.Length; i += 3)
            {
                var elf1 = lines[i];
                var elf2 = lines[i + 1];
                var elf3 = lines[i + 2];

                char badge = ' ';
                foreach(var e in elf1.ToCharArray())
                {
                    if (elf2.Contains(e) && elf3.Contains(e))
                    {
                        badge = e;
                    }
                }
                priority += (int)Enum.Parse(typeof(Priority), badge.ToString());
            }
            return priority;
        }

        enum Priority
        {
            a = 1,
            b = 2,
            c = 3,
            d = 4,
            e = 5, 
            f = 6,
            g = 7,
            h = 8,
            i = 9,
            j = 10,
            k = 11,
            l = 12,
            m = 13,
            n = 14,
            o = 15,
            p = 16,
            q = 17,
            r = 18,
            s = 19,
            t = 20,
            u = 21,
            v = 22,
            w = 23,
            x = 24,
            y = 25,
            z = 26,
            A = 27,
            B = 28,
            C = 29,
            D = 30,
            E = 31, 
            F = 32,
            G = 33,
            H = 34,
            I = 35,
            J = 36,
            K = 37,
            L = 38,
            M = 39,
            N = 40,
            O = 41,
            P = 42,
            Q = 43,
            R = 44,
            S = 45,
            T = 46,
            U = 47,
            V = 48,
            W = 49,
            X = 50,
            Y = 51,
            Z = 52,
        }
    }
}
