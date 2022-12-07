using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AdventOfCode2022.Problems.Day7
{
    internal class Code
    {
        public static int ProblemOne()
        {
            var root = BuildTree();
            return GetSize(root);
        }

        public static int ProblemTwo()
        {
            string[] lines = System.IO.File.ReadAllLines(@"./Problems/Day7/day7.txt");
            foreach(var line in lines)
            {

            }
            return 0;
        }


        private static TreeNode<DirElement> BuildTree()
        {
            string[] lines = System.IO.File.ReadAllLines(@"./Problems/Day7/day7.txt");

            TreeNode<DirElement> root = new TreeNode<DirElement>(new DirElement());
            TreeNode<DirElement> curr = root;

            foreach(var line in lines)
            {
                var parsed = line.Split(' ');
                if (parsed[0] == "$")
                {
                    if (parsed[1] == "cd")
                    {
                        if (parsed[2] == "/")
                        {
                            curr = root;
                        }
                        else if (parsed[2] == "..")
                        {
                            curr = curr.Parent;
                        }
                        else
                        {
                            curr = curr.Children.Where(x => x.Value.Name == parsed[2]).FirstOrDefault();
                        }
                    }
                } else 
                {
                    var ele = new DirElement { Name = parsed[1], Size = 0 };
                    if (parsed[0] != "dir") ele.Size = int.Parse(parsed[0]);
                    curr.AddChild(ele);
                }
            }

            UpdateSizes(root);
            
            return root;
        }

        private static void UpdateSizes(TreeNode<DirElement> node)
        {
            var size = 0;

            if (node.Children.Count == 0)
            {
                return;
            }

            for (var i = 0; i < node.Children.Count; i++)
            {
                UpdateSizes(node[i]);
                size += node[i].Value.Size;
            }

            node.Value = new DirElement{ Name = node.Value.Name, Size = size};
        }

        private static int GetSize(TreeNode<DirElement> node)
        {
            var size = 0;
            var total = 0;
            if (node.Children.Count() == 0)
            {
                return 0;
            }

            foreach(var child in node.Children)
            {
                total += GetSize(child);
                size += child.Value.Size;
            }

            if (size <= 100000)
            {
                return total += size;
            }

            return total;
        }

        private struct DirElement
        {
            public string Name { get; set; }
            public int Size { get; set; }
        }

        // Taken from https://stackoverflow.com/a/10442244
        private class TreeNode<T>
        {
            private T _value;
            private readonly List<TreeNode<T>> _children = new List<TreeNode<T>>();

            public TreeNode(T value)
            {
                _value = value;
            }

            public TreeNode<T> this[int i]
            {
                get { return _children[i]; }
            }

            public TreeNode<T> Parent { get; private set; }

            public T Value { 
                get { return _value; } 
                set { _value = value; } 
            }

            public ReadOnlyCollection<TreeNode<T>> Children
            {
                get { return _children.AsReadOnly(); }
            }

            public TreeNode<T> AddChild(T value)
            {
                var node = new TreeNode<T>(value) {Parent = this};
                _children.Add(node);
                return node;
            }

            public void Traverse(Action<T> action)
            {
                action(Value);
                foreach (var child in _children)
                    child.Traverse(action);
            }
        }
    }
}
