using System;
using System.Collections.Generic;
using System.Linq;

namespace Reconstruction
{
    public class StartUP
    {
        public static void Main()
        {
            var cols = int.Parse(Console.ReadLine());

            var linkedTowns = new List<int>[cols];
            var paths = new int[cols][];
            var buildFee = new string[cols];
            var destroyFee = new string[cols];

            var pathCostSet = new List<int>();
            var totalCost = 0;

            for (var i = 0; i < cols; i++)
            {
                paths[i] = Console.ReadLine().ToArray().Select(x => x - '0').ToArray();
            }

            for (var i = 0; i < cols; i++)
            {
                buildFee[i] = Console.ReadLine();
            }

            for (var i = 0; i < cols; i++)
            {
                destroyFee[i] = Console.ReadLine();
            }

            for (var i = 0; i < cols; i++)
            {
                for (var j = i + 1; j < cols; j++)
                {
                    if (paths[i][j] > 0)
                    {
                        paths[i][j] = ((destroyFee[i][j] - 'A') % 32) + (((destroyFee[i][j] - 'A') / 32) * 26);
                    }
                    else
                    {
                        paths[i][j] -= ((buildFee[i][j] - 'A') % 32) + (((buildFee[i][j] - 'A') / 32) * 26);
                    }

                    BinaryAdd(pathCostSet, paths, (i * cols) + j);
                }
            }

            foreach (var pathCost in pathCostSet)
            {
                if ((linkedTowns[pathCost / cols] == null) || (linkedTowns[pathCost % cols] == null) || !SameTreeCheck(linkedTowns, new bool[cols], pathCost % cols, pathCost / cols))
                {
                    totalCost -= paths[pathCost / cols][pathCost % cols] < 0 ? paths[pathCost / cols][pathCost % cols] : 0;

                    if (linkedTowns[pathCost / cols] == null)
                    {
                        linkedTowns[pathCost / cols] = new List<int>();
                    }

                    if (linkedTowns[pathCost % cols] == null)
                    {
                        linkedTowns[pathCost % cols] = new List<int>();
                    }

                    linkedTowns[pathCost / cols].Add(pathCost % cols);
                    linkedTowns[pathCost % cols].Add(pathCost / cols);
                }
                else
                {
                    totalCost += paths[pathCost / cols][pathCost % cols] > 0 ? paths[pathCost / cols][pathCost % cols] : 0;
                }
            }

            Console.WriteLine(totalCost);
        }

        private static void BinaryAdd(IList<int> pathCostSet, IReadOnlyList<int[]> values, int indexToAdd)
        {
            var cols = values.Count;
            var startIndex = 0;
            var endIndex = pathCostSet.Count - 1;
            var suitablePosition = pathCostSet.Count;

            while (startIndex <= endIndex)
            {
                var currentIndex = (startIndex + endIndex) / 2;

                if (values[pathCostSet[currentIndex] / cols][pathCostSet[currentIndex] % cols] < values[indexToAdd / cols][indexToAdd % cols])
                {
                    endIndex = currentIndex - 1;
                    suitablePosition = currentIndex;
                }
                else if (values[pathCostSet[currentIndex] / cols][pathCostSet[currentIndex] % cols] > values[indexToAdd / cols][indexToAdd % cols])
                {
                    startIndex = currentIndex + 1;
                }
                else
                {
                    suitablePosition = currentIndex;
                    break;
                }
            }

            pathCostSet.Insert(suitablePosition, indexToAdd);
        }

        private static bool SameTreeCheck(IReadOnlyList<IList<int>> linkedTowns, IList<bool> isMarked, int currentIndex, int searchedIndex)
        {
            isMarked[currentIndex] = true;

            for (var i = 0; i < linkedTowns[currentIndex].Count; ++i)
            {
                if (isMarked[linkedTowns[currentIndex][i]])
                {
                    continue;
                }

                if (linkedTowns[currentIndex][i] == searchedIndex || SameTreeCheck(linkedTowns, isMarked, linkedTowns[currentIndex][i], searchedIndex))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
