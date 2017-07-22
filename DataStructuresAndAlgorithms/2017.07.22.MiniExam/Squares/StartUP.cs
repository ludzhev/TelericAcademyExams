using System;
using System.Collections.Generic;
using System.Numerics;

namespace Squares
{
    public class StartUP
    {
        public static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            var m = int.Parse(Console.ReadLine());

            if (n > m)
            {
                var min = m;
                m = n;
                n = min;
            }

            var max = 0;
            var templates = new List<int>();

            for (var i = 0; i < n; i++)
            {
                max |= 1 << i;
            }

            for (var i = 0; i <= max; i++)
            {
                if ((i & (i << 1)) == 0)
                {
                    templates.Add(i);
                }
            }

            var variations = new BigInteger[2, templates.Count];

            for (var i = 0; i < variations.GetLength(1); i++)
            {
                variations[0, i] = 1;
            }

            for (var i = 1; i < m; i++)
            {
                for (var j = 0; j < templates.Count; j++)
                {
                    variations[i % 2, j] = 0;

                    for (var k = 0; k < templates.Count; k++)
                    {
                        if ((templates[j] & templates[k]) == 0)
                        {
                            variations[i % 2, j] += variations[(i + 1) % 2, k];
                        }
                    }
                }
            }

            BigInteger count = 0;

            for (var i = 0; i < variations.GetLength(1); i++)
            {
                count += variations[(m - 1) % 2, i];
            }

            Console.WriteLine(count == 0 ? variations.Length : count);
        }
    }
}
