using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySQLManager;
using MySQLManager.Enums;
using MySQLManager.EventArguments;
using MySQLManager.Helpers;

namespace Delete
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

            manager.ConnectionOpenedSuccessfully += Manager_ConnectionOpenedSuccessfully;
            manager.ConnectionFailedToOpen += Manager_ConnectionFailedToOpen;
            manager.ConnectionClosedSuccessfully += Manager_ConnectionClosedSuccessfully;
            manager.ConnectionFailedToClose += Manager_ConnectionFailedToClose;
            manager.DeletedSuccessfully += Manager_DeletedSuccessfully;
            manager.FailedToDelete += Manager_FailedToDelete;

            Console.WriteLine("Condition Field: ");
            string condName = Console.ReadLine();

            Console.WriteLine("Condition Value: ");
            string condVal = Console.ReadLine();

            Field[] condition =
            {
                new Field(condName,condVal)
            };
            
            manager.Delete("info", condition);

            Console.WriteLine("Pres any key to continue...");
            Console.ReadKey();
        }

        private static void Manager_FailedToDelete(object sender, FailedToDeleteEventArgs e)
        {
            Console.WriteLine(e.ErrorMessage);
        }

        private static void Manager_DeletedSuccessfully(object sender, EventArgs e)
        {
            Console.WriteLine("Row is deleted successfully");
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
