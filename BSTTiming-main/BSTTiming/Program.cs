using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace BSTTiming
{
    class Program : System.Object
    {
        /// <summary>
        /// Duration of one second
        /// </summary>
        public const System.Int32 DURATION = 1000;

        public static System.Int32 SIZE;

        static void Main(System.String[] args)
        {

            System.String line;
using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Jesus Zarate\Desktop\timingResults.txt"))
            {
                line = "Time";
                System.Console.WriteLine(line);
                file.WriteLine(line);

                for (System.Int32 i = 10; i <= 20; i++)
                {
                    SIZE = (System.Int32)System.Math.Pow(2, i);
                    line = RunBSTTiming(SIZE).ToString();

                    System.Console.WriteLine(line);
                    file.WriteLine(line);
                }
            }
            Console.WriteLine("Finished");
            Console.Read();
        }

        public static System.Double RunBSTTiming(System.Int32 size)
        {
            // Construct a randomly-generated balanced
            //binary search tree
            System.Collections.Generic.SortedSet<System.Int32> bst = generateTree(size);

            System.Int32[] items = generateSearchItems(1024);

            // Create a stopwatch
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

            System.Random random = new System.Random();

            // Keep increasing the number of repetitions until one second elapses.
            System.Double elapsed = 0;
            System.Int64 repetitions = 1;
            do
            {
                repetitions *= 2;
                sw.Restart();
                for (System.Int32 i = 0; i < repetitions; i++)
                {
                    for (System.Int32 elt = 0; elt < 1024; elt++)
                    {
                        bst.Contains(items[elt]);
                    }
                }
                sw.Stop();
                elapsed = msecs(sw);
            } while (elapsed < DURATION);
            System.Double totalAverage = elapsed / repetitions;

            // Create a stopwatch
            sw = new System.Diagnostics.Stopwatch();

            // Keep increasing the number of repetitions until one second elapses.
            elapsed = 0;
            repetitions = 1;
            do
            {
                repetitions *= 2;
                sw.Restart();
                for (System.Int32 i = 0; i < repetitions; i++)
                {
                    for (System.Int32 elt = 0; elt < 1024; elt++)
                    {
                    }
                }
                sw.Stop();
                elapsed = msecs(sw);
            } while (elapsed < DURATION);
            System.Double overheadAverage = elapsed / repetitions;

            // Return the difference, averaged over size
            return (totalAverage - overheadAverage) / 1024;
        }

        private static System.Int32[] generateSearchItems(System.Int32 size)
        {
            System.Collections.Generic.HashSet<System.Int32> set = new System.Collections.Generic.HashSet<System.Int32>();
            System.Random random = new System.Random();
            System.Int32 num;
            for(System.Int32 i = 0; i < size; i++)
            {
                do
                {
                    num = random.Next(0, size);
                } while (set.Contains(num));

                set.Add(num);
            }
            return set.ToArray();
        }

        private static System.Collections.Generic.SortedSet<System.Int32> generateTree(System.Int32 size)
        {
            System.Collections.Generic.SortedSet<System.Int32> bst = new System.Collections.Generic.SortedSet<System.Int32>();
            System.Random random = new System.Random();

            System.Int32 number;
            for (System.Int32 i = 0; i < size; i++)
            {
                do
                {
                    number = random.Next(0, size);
                } while (bst.Contains(number));

                bst.Add(number);
            }

            return bst;
        }

        /// <summary>
        /// Returns the number of milliseconds that have elapsed on the Stopwatch.
        /// </summary>
        public static System.Double msecs(System.Diagnostics.Stopwatch sw)
        {
            return (((System.Double)sw.ElapsedTicks) / System.Diagnostics.Stopwatch.Frequency) * 1000;
        }

    }
}