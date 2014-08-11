using CacheDB.Models;
using System;
using System.Collections.Generic;
using System.Threading;

namespace CacheDB.Server
{
    public class Start
    {
        public static Thread t1;
        public static Thread t2;
        private static List<bool> Progress;
        private static int TotalModel;

        public static void StartInstance()
        {
            Progress = new List<bool>();
            t1 = new Thread(StartPerson);
            t1.Start();
            TotalModel++;
            t2 = new Thread(second);
            t2.Start();
            TotalModel++;
        }

        public static bool ServerStarted()
        {
            int count = 0;
            var progressBar = new bool[Progress.Count];
            Array.Copy(Progress.ToArray(), progressBar, Progress.Count);
            foreach (var item in progressBar)
                if (item)
                    count++;
            return count == TotalModel ? true : false;
        }

        private static void StartPerson()
        {
            try
            {
                Console.WriteLine("Inicializando Pessoas");
                Person person = new Person();
                Console.WriteLine("Pessoas -OK");
                Progress.Add(true);
            }
            catch
            {
                Progress.Add(false);
            }
        }

        private static void second()
        {
            int i = 0;
            try
            {
                Console.WriteLine("Inicializando Contador");
                while (i != 5)
                {
                    Thread.Sleep(1000);
                    i++;
                }
                Console.WriteLine("Contador OK");
                Progress.Add(true);
            }
            catch
            {
                Progress.Add(false);
            }
        }
    }
}
