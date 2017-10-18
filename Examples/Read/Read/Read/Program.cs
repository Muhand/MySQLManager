using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySQLManager;
using MySQLManager.Enums;
using MySQLManager.EventArguments;
using MySQLManager.Helpers;

namespace Read
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionCredentials credentials = new ConnectionCredentials
            {
                Server = "127.0.0.1",
                Database = "mysqlmanager",
                Username = "root",
                Password = ""
            };

            CRUDManager manager = new CRUDManager(credentials);
            manager.ConnectionOpenedSuccessfully += Manager_ConnectionOpenedSuccessfully; ;
            manager.ConnectionFailedToOpen += Manager_ConnectionFailedToOpen; ;
            manager.ConnectionClosedSuccessfully += Manager_ConnectionClosedSuccessfully;
            manager.ConnectionFailedToClose += Manager_ConnectionFailedToClose;
            manager.ReadSuccessfully += Manager_ReadSuccessfully;
            manager.FailedToRead += Manager_FailedToRead;

            var res = manager.Read("info");
            foreach (var row in res)
            {
                foreach (var col in row)
                    Console.Write(col + ", ");

                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("READING USING COLUMN NAMES");
            Console.WriteLine("--------------------------");
            Console.WriteLine();

            var res2 = manager.Read("info", "name","age");
            foreach (var row in res2)
            {
                foreach (var col in row)
                    Console.Write(col + ", ");

                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("READING USING CONDITIONS");
            Console.WriteLine("--------------------------");
            Console.WriteLine();

            Field[] c =
            {
                new Field("id","11")
            };
            var res3 = manager.Read("info", c);
            foreach (var row in res3)
            {
                foreach (var col in row)
                    Console.Write(col + ", ");

                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("READING SPECIFIC COLUMNS USING CONDITIONS");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine();

            Field[] c2 =
            {
                new Field("id","11")
            };
            var res4 = manager.Read("info", c,"name","age");
            foreach (var row in res4)
            {
                foreach (var col in row)
                    Console.Write(col + ", ");

                Console.WriteLine();
            }


            Console.WriteLine();
            Console.WriteLine("Pres any key to continue...");
            Console.ReadKey();
        }

        private static void Manager_FailedToRead(object sender, FailedToReadEventArgs e)
        {
            Console.WriteLine(e.ErrorMessage);
        }

        private static void Manager_ReadSuccessfully(object sender, EventArgs e)
        {
            Console.WriteLine("Read successfully");
        }

        private static void Manager_ConnectionFailedToClose(object sender, ConnectionFailedToCloseEventArgs e)
        {
            Console.WriteLine(e.ErrorMessage);
        }

        private static void Manager_ConnectionClosedSuccessfully(object sender, EventArgs e)
        {
            Console.WriteLine("Connection closed successfully");
        }
        private static void Manager_ConnectionFailedToOpen(object sender, ConnectionFailedToOpenEventArgs e)
        {
            Console.WriteLine(e.ErrorMessage);
        }

        private static void Manager_ConnectionOpenedSuccessfully(object sender, EventArgs e)
        {
            Console.WriteLine("Connection was opened successfully");
        }
    }
}
