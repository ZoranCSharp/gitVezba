using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicTest
{
    class Program
    {
        int x;

        struct Test
        {
            int y;
            unsafe static void Main(string[] args)
            {
                dynamic d = new Duck();
                d.Quack();
                d.Waddle();

                Program test = new Program();

                unsafe //Enable using pointers
                {
                    fixed (int* p = &test.x)  //Fiksira test
                    {
                        *p = 9;
                    }
                    Console.WriteLine(test.x);

                    Test test1 = new Test();
                    Test* p1 = &test1;
                    p1->y = 21;
                    System.Console.WriteLine(test1.y);

                }

                Console.WriteLine("\n");
                new UnsafeClass("Christian Troy");

                Console.WriteLine("\n");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Raw raw = new Raw();
                raw.Ispis();

                Console.WriteLine("\n");
                Console.ForegroundColor = ConsoleColor.Blue;
                VoidPokazivac vp = new VoidPokazivac();
                vp.Voider();

                Console.ReadLine();
            }

            public class Duck : DynamicObject
            {
                public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
                {
                    Console.WriteLine("Pozvana je metoda " + binder.Name);
                    result = null;
                    return true;
                }
            }


        }

        unsafe struct UnsafeUnicodeString
        {
            public short Length;
            public fixed byte Buffer[30];
        }

        unsafe class UnsafeClass
        {
            UnsafeUnicodeString uus;

            public UnsafeClass(string s)
            {
                uus.Length = (short)s.Length;
                fixed (byte* p = uus.Buffer)
                    for (int i = 0; i < s.Length; i++)
                    {
                        p[i] = (byte)s[i];
                        Console.WriteLine(p[i]);
                    }
            }
        }


        unsafe class Raw
        {
            public void Ispis()
            {
                unsafe
                {
                    int* a = stackalloc int[10];

                    for (int i = 0; i < 10; i++)
                    {
                        Console.WriteLine(a[i]);
                    }
                }
            }
        }

        unsafe class VoidPokazivac
        {
            public void Voider()
            {
                short[] a = { 1, 1, 2, 3, 5, 8, 13, 21, 34, 55 };

                fixed (short* p = a)
                {
                    //sizeof returns value type in bytes
                    Zap(p, a.Length * sizeof(short));
                }

                foreach (short x in a)
                {
                    System.Console.WriteLine(x);
                }


            }

            unsafe static void Zap(void* memory, int byteCount)
            {
                byte* b = (byte*) memory;
                for (int i = 0; i < byteCount; i++)
                {
                    *b++ = 0;
                }
            }

        }


    }
}
