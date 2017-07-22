using System;

namespace KBase
{
    public class Solution
    {
        public static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            var k = int.Parse(Console.ReadLine());

            var max = 1;
            long count = 0;

            for (var i = 1; i < n; i++)
            {
                max |= 1 << i;
            }

            for (var i = 1; i <= max; i += 2)
            {
                if (((i | (i << 1)) & max) != max)
                {
                    continue;
                }

                var template = i;
                long localCount = 1;

                while (template != 0)
                {
                    localCount *= (template & 1) == 0 ? 1 : (template & 1) * (k - 1);
                    template >>= 1;
                }

                count += localCount;
            }

            Console.WriteLine(count);
        }
    }
}
