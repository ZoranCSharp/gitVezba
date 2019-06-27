using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LambdaCSharp
{

    class PriceChangedEventArgs : EventArgs
    {
        public readonly decimal LastPrice;
        public readonly decimal NewPrice;

        public PriceChangedEventArgs(decimal lastPrice, decimal newPrice)
        {
            LastPrice = lastPrice;
            NewPrice = newPrice;
        }
    }

    class Stock
    {
        string symbol;
        decimal price;

        public Stock(string symbol)
        {
            this.symbol = symbol;
        }

        public event EventHandler<PriceChangedEventArgs> PriceChanged;

        protected virtual void OnPriceChanged(PriceChangedEventArgs e)
        {
            PriceChanged?.Invoke(this, e);
        }

        public decimal Price
        {
            get { return price; }
            set
            {
                if (price == value) return;
                decimal oldPrice = price;
                price = value;
                OnPriceChanged(new PriceChangedEventArgs(oldPrice, price));

            }
        }
    }

    class Exep
    {
        public void Blah()
        {
            string s = null;
            using (WebClient wc = new WebClient())
                try
                {
                    s = wc.DownloadString("http://albahari.com/nutshell/");
                }
                //catch(WebException ex)
                //{
                //    if(ex.Status == WebExceptionStatus.Timeout)
                //    {
                //        Console.WriteLine("tajmaut");
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}
                catch(WebException ex) when (ex.Status == WebExceptionStatus.Timeout)
                {
                    Console.WriteLine("tajmaut");
                }



        }
    }

    class Iteratori
    {
        public void Iterat()
        {
            //foreach (int fib in Fibs(10))
            //{
                // Console.WriteLine(fib + " ");
                foreach (int fib in EvenNumbers(Fibs(10)))
                {
                Console.WriteLine(fib);
                }
            //}
        }

        static IEnumerable<int> Fibs(int fibCount)
        {
            for(int i = 0, prevFib = 1, curFib = 1; i < fibCount; i++)
            {
                yield return prevFib;
                int newFib = prevFib + curFib;
                prevFib = curFib;
                curFib = newFib;
            }
        }

         static IEnumerable<int> EvenNumbers (IEnumerable<int> sequence)
        {
            foreach (int num in sequence)
            {
                if((num % 2) == 0)
                {
                    yield return num;
                }
            }
        }
    }

   

   
    class Program
    {
        static (string name, int age) GetPerson() => ("Bob", 25);

        static void Main(string[] args)
        {
            

            Stock stock = new Stock("THPW");
            stock.Price = 27.10M;

            stock.PriceChanged += stock_PriceChanged;
            stock.Price = 31.59M; 

            int factor = 2;
            Func<int, int> multiplier = n => n * factor;

            Console.WriteLine(multiplier(3));

            int factor2 = 2;
            Func<int, int> rez = n => n * factor2;
            factor2 = 10;
            Console.WriteLine(rez(3));

            Func<int> natural = Natural();
            Console.WriteLine(natural());
            Console.WriteLine(natural());

            Exep ex = new Exep();
            ex.Blah();

            Console.ForegroundColor = ConsoleColor.Blue;
            Iteratori iter = new Iteratori();
            iter.Iterat();

            int? a = null;
            int? b = 5;
            Console.WriteLine(a == null);
            Console.WriteLine(a == b);
            Console.WriteLine(a != 5);
            Console.WriteLine(b == null);
            Console.WriteLine(b > 10);
            Console.ForegroundColor = ConsoleColor.Red;
            bool x = (b.HasValue && a.HasValue) ? (a.Value > b.Value) : false;
            Console.WriteLine(x);

            bool? aa = null;
            bool? bb = false;
            Console.WriteLine(aa | bb);
            Console.WriteLine(aa & bb);

            var tuple = (Name: "Bob", Age: 25);
            Console.WriteLine(tuple.Age);
            Console.WriteLine(tuple.Name);

            Console.ForegroundColor = ConsoleColor.Yellow;
            var person = GetPerson();
            Console.WriteLine(person.name);
            Console.WriteLine(person.age);


            Console.ReadKey();

        }

        static Func<int> Natural()
        {
            int seed = 0;
            return () => seed++;
        }
        static void stock_PriceChanged(object sender, PriceChangedEventArgs e)
        {
            if ((e.NewPrice - e.LastPrice) / e.LastPrice > 0.1M)
            {
                Console.WriteLine("Alarm, price has become bigger for 10%!");
            }
        }

    }
}
