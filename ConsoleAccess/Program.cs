using System;
using System.Threading;

namespace ConsoleAccess
{
    class Program
    {
        private static Thread t1;
        private static Thread t2;

        static void Main(string[] args)
        {
            Init.Start();
            t1 = new Thread(Application.AddPersons);
            t1.Start();
            t2 = new Thread(Application.AddPersons);
            t2.Start();
            Console.WriteLine("Verificar se o processo do BD já acabou");
            Console.ReadKey();
            foreach (var item in CacheDB.Models.Person.Verify(25))
                Console.WriteLine(string.Format("Id:{0} - Name:{1}", item.Id, item.Name));
            Console.ReadKey();
        }
    }
}
