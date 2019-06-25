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

   
    class Program
    {

        
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
