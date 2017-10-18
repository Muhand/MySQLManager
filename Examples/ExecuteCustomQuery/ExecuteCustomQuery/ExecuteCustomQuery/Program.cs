using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySQLManager;
using MySQLManager.Enums;
using MySQLManager.EventArguments;
using MySQLManager.Helpers;

namespace ExecuteCustomQuery
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
            manager.CustomQueryExecutedSuccessfully += Manager_CustomQueryExecutedSuccessfully;
            manager.FailedToExecuteCustomQuery += Manager_FailedToExecuteCustomQuery;


            Console.WriteLine();
            Console.WriteLine("ExecutionOptions.ExecuteReader");
            Console.WriteLine("--------------------------------");
            Console.WriteLine();

            string query = "SELECT * FROM info WHERE name='Muhand Jumah 3' AND age = '22'";
            List<List<string>> res = new List<List<string>>();
            manager.ExecuteCustomQuery(query,ExecutionOptions.ExecuteReader, out res);

            Console.WriteLine();
            
            foreach (var row in res)
            {
                foreach (var col in row)
                    Console.Write(col+", ");

                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("ExecutionOptions.ExecuteNonQuery");
            Console.WriteLine("--------------------------------");
            Console.WriteLine();
            
            string query2 = "INSERT INTO info (name,age) VALUES ('Muhand Jumah 3','22')";
            manager.ExecuteCustomQuery(query2, ExecutionOptions.ExecuteNonQuery);


            Console.WriteLine("Pres any key to continue...");
            Console.ReadKey();
        }

        private static void Manager_FailedToExecuteCustomQuery(object sender, FailedToExecuteCustomQueryEventArgs e)
        {
            Console.WriteLine(e.ErrorMessage);
        }

        private static void Manager_CustomQueryExecutedSuccessfully(object sender, EventArgs e)
        {
            Console.WriteLine("Custom Query has been executed");
        }

        private static void Manager_ConnectionFailedToClose(object sender, ConnectionFailedToCloseEventArgs e)
        {
            Console.WriteLine(e.ErrorMessage);
        }

        private static void Manager_ConnectionClosedSuccessfully(object sender, EventArgs e)
        {
            Console.WriteLine("Connection closed successfully");
        }

        private static void Manager_FailedToCreate(object sender, FailedToCreateEventArgs e)
        {
            Console.WriteLine(e.ErrorMessage);
        }

        private static void Manager_CreatedSuccessfully(object sender, EventArgs e)
        {
            Console.WriteLine("A new row is created successfully");
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
