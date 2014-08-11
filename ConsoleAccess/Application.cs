using CacheDB.Models;
using System;

namespace ConsoleAccess
{
    class Application
    {
        public static void ListPersons()
        {
            foreach (var person in Person.Collection)
                Console.WriteLine(string.Format("Id:{0}\nName:{1}", person.Id, person.Name));
        }

        public static void AddPersons()
        {
            Random rnd = new Random();
            for (int i = 0; i < 15; i++)
                Person.AddItem(new Person("Nome N" + rnd.Next(1, 100).ToString()));
        }
    }
}
