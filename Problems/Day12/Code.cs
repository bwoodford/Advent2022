using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2022.Problems.Day12
{
    internal class Code
    {
        public static int ProblemOne()
        {
            var graph = BuildGraph();
            return graph.FindShortestPath();
        }

        public static int ProblemTwo()
        {
            var graph = BuildGraph();
            var answer = int.MaxValue;
            
            foreach (var point in graph.StartPoints)
            {
                graph.Start = point;
                var distance = graph.FindShortestPath();
                if (distance < answer) answer = distance;
                graph.ResetGraph();
            }
            return answer;
        }

        internal static Graph BuildGraph()
        {
            var lines = System.IO.File.ReadAllLines(@"./Problems/Day12/day12.txt");
            List<List<Node>> nodes = new List<List<Node>>();
            var graph = new Graph();
            var points = new List<Node>();

            for (var i = 0; i < lines.Length; i++)
            {
                nodes.Add(new List<Node>());
                var chars = lines[i].ToCharArray();
                foreach (var c in chars)
                {
                    var node = new Node(c);
                    if (c == 'E')
                    {
                        node.Name = 'z';
                        graph.End = node;
                    }
                    else if (c == 'S')
                    {
                        node.Name = 'a';
                        graph.Start = node;
                    }

                    if (node.Name == 'a') points.Add(node);
                    nodes[i].Add(node);
                }
            }

            for (var i = 0; i < nodes.Count; i++)
            {
                for (var j = 0; j < nodes[i].Count; j++)
                {
                    var curr = nodes[i][j];
                    var calc = 0;

                    if (i - 1 >= 0) 
                    {
                        calc = nodes[i - 1][j].Name - curr.Name;
                        if (calc <= 1) curr.AddEdge(nodes[i - 1][j]);
                    }
                    if (i + 1 < nodes.Count) 
                    {
                        calc = nodes[i + 1][j].Name - curr.Name;
                        if (calc <= 1) curr.AddEdge(nodes[i + 1][j]);
                    }
                    if (j - 1 >= 0) 
                    {
                        calc = nodes[i][j - 1].Name - curr.Name;
                        if (calc <= 1) curr.AddEdge(nodes[i][j - 1]);
                    }
                    if (j + 1 < nodes[i].Count) {
                        calc = nodes[i][j + 1].Name - curr.Name;
                        if (calc <= 1) curr.AddEdge(nodes[i][j + 1]);
                    }
                }
            }

            graph.AllNodes = nodes.SelectMany(x => x).ToList();
            graph.StartPoints = points;

            return graph;
        }
    }

    internal class Node
    {
        public char Name;
        public int Distance;
        public bool Visited = false;
        public List<Edge> Edges = new List<Edge>();

        public Node(char name)
        {
            Name = name;
        }

        public Node AddEdge(Node child)
        {
            Edges.Add(new Edge 
            {
                Child = child,
            });
            return this;
        }
    }

    internal class Edge 
    {
        public Node Child;
    }

    internal class Graph
    {
        public Node Start;
        public Node End;

        // Only for part 2
        public List<Node> StartPoints;
        public List<Node> AllNodes;

        public int FindShortestPath()
        {
            var queue = new Queue<Node>();
            queue.Enqueue(Start);
            Node node = null;

            while (queue.Count > 0 && node != End)
            {
                node = queue.Dequeue();
                node.Visited = true;
                foreach(var edge in node.Edges)
                {
                    if (edge.Child.Visited || queue.Contains(edge.Child)) continue;
                    edge.Child.Distance = node.Distance + 1;
                    queue.Enqueue(edge.Child);
                }
            }
            if (node != End) return int.MaxValue;
            return node.Distance;
        }

        public void ResetGraph()
        {
            foreach (var node in AllNodes)
            {
                node.Visited = false; 
                node.Distance = 0;
            }
        }
    }
}
