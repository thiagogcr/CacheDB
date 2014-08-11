using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CacheDB.Models
{
    public class Person : DB
    {
        #region PublicMehtods
        public int Id { get; set; }
        public string Name { get; set; }
        public static List<Person> Collection { get; set; }
        public static Thread tInsert;
        public static Thread tLastId;
        private static int lastId;
        private static List<Person> CacheCollection { get; set; }
        private bool HasProcessed { get; set; }

        public Person()
        {
            Collection = FindAll();
            CacheCollection = new List<Person>();
            Thread t = new Thread(() => Insert());
            t.Start();
        }

        private Person(int Id, string name)
        {
            this.Id = Id;
            this.Name = name;
            this.HasProcessed = false;
        }

        public Person(string name)
        {
            this.Name = name;
            this.HasProcessed = false;
        }

        public static void AddItem(Person person)
        {
            GetNextId();
            person.Id = lastId;
            Collection.Add(person);
            CacheCollection.Add(person);
        }

        public static List<Person> Verify(int id)
        {
            var list = new List<Person>();
            list.Add(Collection.Where(p => p.Id == id).FirstOrDefault());


            var SQL = "Select * from person where id =" + id;
            lock (myConnection)
            {
                myConnection.Open();
                MySqlCommand myCommand = new MySqlCommand(SQL, myConnection);
                MySqlDataReader myReader = myCommand.ExecuteReader();
                myCommand.Dispose();
                if (myReader.HasRows)
                    while (myReader.Read())
                    {
                        var person = new Person((int)myReader["Id"], myReader["Name"].ToString());
                        list.Add(person);
                    }
                CloseConnection();
                myCommand.Dispose();
            }
            return list;
        }
        #endregion PublicMehtods

        #region PrviateMehtods
        private static List<Person> FindAll()
        {
            var list = new List<Person>();
            var SQL = "Select * from person";
            lock (myConnection)
            {
                myConnection.Open();
                MySqlCommand myCommand = new MySqlCommand(SQL, myConnection);
                MySqlDataReader myReader = myCommand.ExecuteReader();
                myCommand.Dispose();
                if (myReader.HasRows)
                    while (myReader.Read())
                    {
                        var person = new Person((int)myReader["Id"], myReader["Name"].ToString());
                        list.Add(person);
                    }
                CloseConnection();
                myCommand.Dispose();
            }
            return list;
        }

        private static void Insert()
        {
            while (true)
            {
                lock (CacheCollection)
                {
                    for (int i = 0; i < CacheCollection.Count(); i++)
                    {
                        var person = CacheCollection[i];
                        if (!person.HasProcessed)
                        {
                            lock (myConnection)
                            {
                                Thread.Sleep(1200);
                                myConnection.Open();
                                var strInsertSQL = "INSERT INTO `person` (`Id`,`Name`) VALUES ('" + person.Id + "', '" + person.Name + "')";
                                MySqlCommand cmdInserttblProductFrance = new MySqlCommand(strInsertSQL, myConnection);
                                cmdInserttblProductFrance.ExecuteNonQuery();
                                CloseConnection();
                                person.HasProcessed = true;
                            }
                        }
                    }
                }
                CacheCollection.Clear();
            }
        }

        private static void GetNextId()
        {
            lock (Collection)
                lastId = Collection.Count() > 0 ? Collection.Last().Id + 1 : 1;

        }
        #endregion PrviateMehtods
    }
}

