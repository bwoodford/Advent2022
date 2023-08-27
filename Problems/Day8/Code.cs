using System.Collections.Generic;

namespace AdventOfCode2022.Problems.Day8 
{
    internal class Code
    {
        public static int ProblemOne()
        {
            var map = GetMap();
            return map.Count * 2 + map[0].Count * 2 - 4 + GetScore(map);
        }


        public static int ProblemTwo()
        {
            var map = GetMap();
            return GetScenicScore(map);
        }

        private static int GetScore(List<List<MapMember>> map)
        {
            var answer = 0;

            // Horizontal
            for (var i = 0; i < map[0].Count; i++)
            {
                var j = 0;
                var lMax = 0;
                while (j < map[i].Count)
                {
                    if (lMax < map[i][j].Value)
                    {
                        lMax = map[i][j].Value;
                        if(!map[i][j].Visited) answer++;
                        map[i][j].Visited = true;
                    }
                    j++;
                }

                j = map[i].Count - 1;
                var rMax = 0;
                while (j >= 0 && lMax != rMax)
                {
                    if (rMax < map[i][j].Value)
                    {
                        rMax = map[i][j].Value;
                        if(!map[i][j].Visited) answer++;
                        map[i][j].Visited = true;
                    }
                    j--;
                }
            }

            // Vertical
            for (var j = 0; j < map.Count; j++)
            {
                var i = 0;
                var tMax = 0;
                while (i < map.Count)
                {
                    if (tMax < map[i][j].Value)
                    {
                        tMax = map[i][j].Value;
                        if(!map[i][j].Visited) answer++;
                        map[i][j].Visited = true;
                    }
                    i++;
                }

                i = map.Count - 1;
                var bMax = 0;
                while (i >= 0 && tMax != bMax)
                {
                    if (bMax < map[i][j].Value)
                    {
                        bMax = map[i][j].Value;
                        if(!map[i][j].Visited) answer++;
                        map[i][j].Visited = true;
                    }
                    i--;
                }
            }

            return answer;
        }
        
        private static int GetScenicScore(List<List<MapMember>> map)
        {
            var topScore = 0;
            
            for (var i = 0; i < map.Count; i++)
            {
                for (var j = 0; j < map[i].Count; j++)
                {
                    var tI = i;
                    var tJ = j;
                    var count = 0;
                    var tmp = 1;
                    var height = map[i][j].Value;

                    // top
                    while (tI > 0 && map[tI][tJ].Value <= height)
                    {
                        count += 1;
                        tI--;
                    }

                    tI = i;
                    tJ = j;
                    tmp *= count;
                    count = 0;

                    // bottom 
                    while (tI < map.Count && map[tI][tJ].Value <= height)
                    {
                        count += 1;
                        tI++;
                    }

                    tI = i;
                    tJ = j;
                    tmp *= count;
                    count = 0;

                    // left 
                    while (tJ > 0 && map[tI][tJ].Value <= height)
                    {
                        count += 1;
                        tJ--;
                    }

                    tI = i;
                    tJ = j;
                    tmp *= count;
                    count = 0;

                    // right 
                    while (tJ < map.Count && map[tI][tJ].Value <= height)
                    {
                        count += 1;
                        tJ++;
                    }

                    tmp *= count;
                    
                    if (tmp > topScore)
                    {
                        topScore = tmp;
                    }
                }
            }
            return topScore;
        }


        private static List<List<MapMember>> GetMap()
        {
            string[] lines = System.IO.File.ReadAllLines(@"./Problems/Day8/test.txt");
            var map = new List<List<MapMember>>();

            var length = lines.Length;

            for(int i = 0; i < length; i++)
            {
                map.Add(new List<MapMember>());
                var line = lines[i].ToCharArray();
                for(int j = 0; j < line.Length; j++) {
                    var visited = (i == 0 || j == 0 || i == length - 1 || j == length - 1) ? true : false;
                    // https://stackoverflow.com/a/239107
                    map[i].Add(new MapMember { Value = line[j] - '0', Visited = visited});
                }
            }

            return map;
        }

        private class MapMember
        {
            public int Value { get; set; }
            public bool Visited { get; set; }
        }
    }
}
