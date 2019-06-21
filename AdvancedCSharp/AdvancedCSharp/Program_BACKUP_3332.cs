using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCSharp
{
    class Program
    {
        public delegate int Transformer(int x);

        public delegate void ProgressReporter(int percentComplete);

        class Util
        {
            public static void Transform(int[] values, Transformer t)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = t(values[i]);
                }
            }

            public static void HardWork(ProgressReporter p)
            {
                for (int i = 0; i < 10; i++)
                {
                    p(i * 10);
                    System.Threading.Thread.Sleep(100);
                }
            }
        }

        //MAIN
        static void Main(string[] args)
        {
            Transformer t = Square;
            int result = t(5);

            Console.WriteLine(result);

            int[] values = { 1, 2, 3 };
            Util.Transform(values, Square);
            foreach (var number in values)
            {
                Console.Write(number + " ");
            }

            ProgressReporter p = WriteProgressToConsole;
            p += WriteProgressToFile;
            Util.HardWork(p);

            X x = new X();
            ProgressReporter p1 = x.InstancePRogress;
            p1(99);
            Console.WriteLine(p1.Target == x);
            Console.WriteLine(p1.Method);


            Console.ReadLine();
        }
        //END MAIN

<<<<<<< HEAD

=======
>>>>>>> 01a21d5c384797b190c6bad2c6746ccc2e0a4d61
        static int Square(int x)
        {
            return x * x;
        }

        static void WriteProgressToConsole(int percentComplete)
        {
            Console.WriteLine(percentComplete);
        }

        static void WriteProgressToFile(int percentComplete)
        {
            System.IO.File.WriteAllText("progress.txt", percentComplete.ToString());
        }

        class X
        {
            public void InstancePRogress(int percentComplete)
            {
                Console.WriteLine(percentComplete);
            }
        }
    }
}