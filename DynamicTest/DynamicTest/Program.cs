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
        static void Main(string[] args)
        {
            dynamic d = new Duck();
            d.Quack();
            d.Waddle();

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
}
