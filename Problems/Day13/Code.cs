
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace AdventOfCode2022.Problems.Day13
{
    internal class Code
    {
        public static int ProblemOne()
        {
            var containers = GetContainedPackets();
            var answer = 0;

            for (var i = 0; i < containers.Count; i++)
            {
                if (ComparePackets(containers[i].Left , containers[i].Right) == -1)
                {
                    answer += (i + 1);
                }
            }

            return answer;
        }

        public static int ProblemTwo()
        {
            return 0;
        }


        private static List<Container> GetContainedPackets()
        {
            var lines = System.IO.File.ReadAllLines(@"./Problems/Day13/day13.txt").ToList();
            lines.RemoveAll(x => x == "");

            List<Container> containers = new List<Container>();
            Container container = new Container();

            for (var i = 0; i < lines.Count; i++)
            {
                var parsed = Packet.FromJsonElement((JsonElement)JsonSerializer.Deserialize<object>(lines[i]));
                if (i % 2 == 0)
                {
                    container = new Container();
                    container.Left = parsed;
                } else
                {
                    container.Right = parsed;
                    containers.Add(container);
                }
            }
            return containers;
        }

        private static int ComparePackets(Packet left, Packet right)
        {
            if (left is NumberPacket nLeft && right is NumberPacket nRight)
            {
                return nLeft.Value.CompareTo(nRight.Value);
            } else if (left is ListPacket lLeft && right is ListPacket lRight) {
                var i = 0;
                while (i < lLeft.Value.Length && i < lRight.Value.Length)
                {
                    var cp = ComparePackets(lLeft.Value[i], lRight.Value[i]);
                    if (cp == -1) return -1;
                    if (cp == 1) return 1;
                    i++;
                }
                if (i == lLeft.Value.Length && i == lRight.Value.Length)
                {
                    return 0;
                } else if (i == lLeft.Value.Length)
                {
                    return -1;
                } else 
                {
                    return 1;
                }
            } else
            {
                if (left is NumberPacket eLeft)
                {
                    return ComparePackets(new ListPacket(new Packet[] { eLeft }), right);
                } else if (right is NumberPacket eRight)
                {
                    return ComparePackets(left, new ListPacket(new Packet[] { eRight }));
                }
            }
            return 0;
        }

        internal class Packet
        {
            public static Packet FromJsonElement(JsonElement element) =>
                element.ValueKind switch
                {
                    JsonValueKind.Number => new NumberPacket(element.GetInt32()),
                    JsonValueKind.Array => new ListPacket(element.EnumerateArray().Select(FromJsonElement).ToArray()),
                    _ => throw new NotImplementedException(),
                };
        }

        internal struct Container 
        {
            public Packet Left { get; set; }
            public Packet Right { get; set; }
        }

        internal class ListPacket: Packet
        {
            public Packet[] Value { get; set; }
            public ListPacket(Packet[] packets)
            {
                Value = packets;
            }
        }

        internal class NumberPacket: Packet
        {
            public int Value { get; set; }
            public NumberPacket(int number)
            {
                Value = number;
            }
        }
     
    }
}
