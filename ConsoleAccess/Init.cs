using System;
using System.Threading;

namespace ConsoleAccess
{
    public static class Init
    {
        public static void Start()
        {
            CacheDB.Server.Start.StartInstance();
            bool flag = true;
            Thread.Sleep(1000);
            while (flag)
            {
                if (CacheDB.Server.Start.ServerStarted())
                {
                    flag = false;
                    Console.WriteLine("Server Iniciado, pressione uma tecla para continuar");
                    Thread.Sleep(1000);
                    Console.WriteLine();
                }
                else
                    Console.WriteLine("Inciado servidor, aguarde...");
                Thread.Sleep(1000);
            }
        }
    }
}
