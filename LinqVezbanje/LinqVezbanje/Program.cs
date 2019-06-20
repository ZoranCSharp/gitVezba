using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqVezbanje
{
    class Program
    {
        static void Main(string[] args)
        {

            IEnumerable<string> query = from f in FontFamily.Families
                                        select f.Name;

            IEnumerable<string> query2 = FontFamily.Families.Select(n => n.Name);

            var query3 = from f in FontFamily.Families
                         select new { f.Name, LineSpacing = f.GetLineSpacing(FontStyle.Bold) };

            foreach (var name in query3)
            {
                Console.WriteLine(name);
            }

            IEnumerable<int> Totals(List<int> numbers)
            {
                var total = 0;
                foreach (var number in numbers)
                {
                    total += number;
                    yield return total;
                }
            }

            foreach (var total in Totals(new List<int> { 1, 2, 3, 4, 5 }))
            {
                Console.WriteLine(total);
            }

            ShowGalaxies();

            Console.WriteLine("\n");

            Vezbanje novo = new Vezbanje();
            novo.ImeVezba();

            Console.WriteLine("\n");
            novo.ImeVezba2();

            Console.WriteLine("\n");
            Trazi trazi = new Trazi();
            trazi.Find();

            Console.WriteLine("\n");
            Selectmany mnogo = new Selectmany();
            mnogo.SelectMany();

            Console.WriteLine("\n");
            Zip zip = new Zip();
            zip.ZippityZip();


            Console.ReadLine();
        }

        public class Galaxy
        {
            public String Name { get; set; }
            public int MegaLigtYears { get; set; }
        }

        public class Galaxies
        {
            public System.Collections.Generic.IEnumerable<Galaxy> NextGalaxy
            {
                get
                {
                    yield return new Galaxy { Name = "TadPole", MegaLigtYears = 400 };
                    yield return new Galaxy { Name = "Pinwheel", MegaLigtYears = 25 };
                    yield return new Galaxy { Name = "Milky Way", MegaLigtYears = 0 };
                    yield return new Galaxy { Name = "Andromeda", MegaLigtYears = 3 };
                }
            }
        }

        public static void ShowGalaxies()
        {
            var theGalaxies = new Galaxies();

            foreach (Galaxy theGalaxy in theGalaxies.NextGalaxy)
            {
                Debug.WriteLine(theGalaxy.Name + " " + theGalaxy.MegaLigtYears.ToString());

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Name of Galaxy is: {theGalaxy.Name} and it's distance from Earth is: {theGalaxy.MegaLigtYears}");
                
            }


        }
    }

    class Vezbanje
    {
        string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" };
        public void ImeVezba()
        {            

            IEnumerable<string> query = names.Where(n => n.EndsWith("y"));

            foreach (var name in query)
            {
                Console.WriteLine(name);
            }
        }

        public void ImeVezba2()
        {
            IEnumerable<string> query = from a in names
                                        where a.EndsWith("y")
                                        select a;
            foreach (var name in query)
            {
                Console.WriteLine(name);
            }
        }
    }

    class Trazi
    {
        public void Find()
        {
            DirectoryInfo[] dirs = new DirectoryInfo(@"C:\Users\zoran.kovacevic\Desktop").GetDirectories();

            var query = from d in dirs
                        where (d.Attributes & FileAttributes.System) == 0
                        select new
                        {
                            DirectoryName = d.FullName,
                            Created = d.CreationTime,
                            Files = from f in d.GetFiles()
                                    where (f.Attributes & FileAttributes.Hidden) == 0
                                    select new
                                    {
                                        FileName = f.Name, f.Length
                                    }
                        };

            foreach (var dirFiles in query)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Directory: " + dirFiles.DirectoryName);
                foreach (var file in dirFiles.Files)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("  " + file.FileName + " Length: " + file.Length);
                }
            }
        }
    }

    class Selectmany
    {
        public void SelectMany()
        {
            string[] names = { "Anne Williams", "John Fred Smith", "Sue Green" };

            IEnumerable<string> query = names.SelectMany(n => n.Split());

            foreach (var name in query)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(name);
            }
        }
    }

    class Zip
    {
        public void ZippityZip()
        {
            int[] numbers = { 3, 5, 7 };
            string[] words = { "three", "five", "seven" };

            IEnumerable<string> query = numbers.Zip(words, (n,w)=>n +"="+w);

            foreach (var name in query)
            {
                Console.WriteLine(name);
            }
        }
    }

    
}

