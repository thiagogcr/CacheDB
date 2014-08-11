using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static List<Person> FindAll()
        {
            var list = new List<Person>();
            return list;
        }
    }
}
