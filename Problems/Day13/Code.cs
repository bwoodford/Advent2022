
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
            var packets = GetPackets();
            var groups = packets.Select((value, index) => new { value, index })
                                .GroupBy(x => x.index / 2, x => x.value);
            var answer = 0;

            var i = 1;
            foreach (var group in groups)
            {
                if (group.First().CompareTo(group.Last()) == -1)
                {
                    answer += i;
                }
                i++;
            }

            return answer;
        }

        public static int ProblemTwo()
        {
            var packets = GetPackets();
            var packet1 = Packet.FromString("[[2]]");
            var packet2 = Packet.FromString("[[6]]");
            packets.Add(packet1);
            packets.Add(packet2);
            packets.Sort();

            var answer = 1;
            
            for(var i = 0; i < packets.Count; i++)
            {
                if (packets[i].Equals(packet1) || packets[i].Equals(packet2))
                {
                    answer *= (i + 1);
                }
            }
            return answer;
        }

        private static List<Packet> GetPackets()
        {
            var lines = System.IO.File.ReadAllLines(@"./Problems/Day13/day13.txt").ToList();
            lines.RemoveAll(x => x == "");
            List<Packet> packets = new List<Packet>();

            for (var i = 0; i < lines.Count; i++)
            {
                packets.Add(Packet.FromString(lines[i]));
            }

            return packets;
        }
        
        internal class Packet: IComparable<Packet>
        {
            public static Packet FromString(string json) => 
                Packet.FromJsonElement((JsonElement)JsonSerializer.Deserialize<object>(json));

            public static Packet FromJsonElement(JsonElement element) =>
                element.ValueKind switch
                {
                    JsonValueKind.Number => new NumberPacket(element.GetInt32()),
                    JsonValueKind.Array => new ListPacket(element.EnumerateArray().Select(FromJsonElement).ToArray()),
                    _ => throw new NotImplementedException(),
                };

            public int CompareTo(Packet right)
            {
                if (this is NumberPacket nLeft && right is NumberPacket nRight)
                {
                    return nLeft.Value.CompareTo(nRight.Value);
                } else if (this is ListPacket lLeft && right is ListPacket lRight) {
                    var i = 0;
                    while (i < lLeft.Value.Length && i < lRight.Value.Length)
                    {
                        var cp = lLeft.Value[i].CompareTo(lRight.Value[i]);
                        if (cp != 0) return cp;
                        i++;
                    }
                    if (i == lRight.Value.Length && i < lLeft.Value.Length)
                    {
                        return 1;
                    } else if (i == lLeft.Value.Length && i < lRight.Value.Length)
                    {
                        return -1;
                    } else 
                    {
                        return 0;
                    }
                } else if (this is NumberPacket eLeft)
                {
                    return new ListPacket(new Packet[] { eLeft }).CompareTo(right);
                } else 
                {
                    return this.CompareTo(new ListPacket(new Packet[] { (NumberPacket)right }));
                }
            }
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
