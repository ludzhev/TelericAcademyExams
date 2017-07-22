using System;
using System.Collections.Generic;
using System.Linq;

namespace Reconstruction
{
    public class Solution
    {
        public static void Main()
        {
            var cols = int.Parse(Console.ReadLine());

            var tree = new UnionFind(cols);
            var paths = new bool[cols][];
            var traces = new List<Tuple<int, int>>();

            for (var i = 0; i < cols; i++)
            {
                paths[i] = Console.ReadLine().ToArray().Select(x => (x - '0') > 0).ToArray();
            }

            for (var i = 0; i < cols; i++)
            {
                var line = Console.ReadLine();

                for (var j = i + 1; j < line.Length; j++)
                {
                    if (paths[i][j]) continue;
                    var trace = new Tuple<int, int>(((line[j] - 'A') % 32 + ((line[j] - 'A') / 32) * 26), i * cols + j);
                    traces.Add(trace);
                }
            }

            for (var i = 0; i < cols; i++)
            {
                var line = Console.ReadLine();

                for (var j = i + 1; j < line.Length; j++)
                {
                    if (!paths[i][j]) continue;
                    var trace = new Tuple<int, int>(-((line[j] - 'A') % 32 + ((line[j] - 'A') / 32) * 26), i * cols + j);
                    traces.Add(trace);
                }
            }

            traces.Sort();
            var price = 0;

            foreach (var trace in traces)
            {
                var toCreate = 0 <= trace.Item1;

                if (tree.InTheSameSet(trace.Item2 / cols, trace.Item2 % cols))
                {
                    if (!toCreate)
                    {
                        price -= trace.Item1;
                    }
                }
                else
                {
                    tree.Union(trace.Item2 / cols, trace.Item2 % cols);

                    if (toCreate)
                    {
                        price += trace.Item1;
                    }
                }

                if (!tree.IsInSet(trace.Item2 / cols) && !tree.IsInSet(trace.Item2 % cols) && toCreate)
                {
                    break;
                }
            }

            Console.WriteLine(price);
        }

        public class UnionFind
        {
            private readonly int[] array;

            public UnionFind(int n)
            {
                array = new int[n];
                for (var i = 0; i < n; i++)
                {
                    array[i] = -1;
                }
            }

            public int Find(int x)
            {
                return array[x] < 0 ? x : array[x] = Find(array[x]);
            }

            public bool Union(int x, int y)
            {
                x = Find(x);
                y = Find(y);
                if (x == y)
                {
                    return false;
                }
                array[x] = y;
                return true;
            }

            public bool InTheSameSet(int x, int y)
            {
                return Find(x) == Find(y);
            }

            public bool IsInSet(int x)
            {
                return 0 <= array[x];
            }
        }
    }
}
