using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySQLManager;
using MySQLManager.EventArguments;
using MySQLManager.Helpers;
using MySQLManager.Enums;

namespace Update
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
            manager.UpdatedSuccessfully += Manager_UpdatedSuccessfully;
            manager.FailedToUpdate += Manager_FailedToUpdate;

            Console.WriteLine("Field Name: ");
            string fieldName = Console.ReadLine();

            Console.WriteLine("New Value: ");
            string fieldValue = Console.ReadLine();

            Console.WriteLine("Condition - Field Name: ");
            string conditionFieldName = Console.ReadLine();

            Console.WriteLine("Condition - Value: ");
            string conditionValue = Console.ReadLine();

            Field[] fields ={
                new Field(fieldName, fieldValue)
            };

            Field[] conditions ={
                new Field(conditionFieldName, conditionValue.ToString())
            };

            manager.Update("info", fields, conditions);

            Console.WriteLine("Pres any key to continue...");
            Console.ReadKey();
        }

        private static void Manager_FailedToUpdate(object sender, FailedToUpdateEventArgs e)
        {
            Console.WriteLine(e.ErrorMessage);
        }

        private static void Manager_UpdatedSuccessfully(object sender, EventArgs e)
        {
            Console.WriteLine("Connection closed successfully");
        }

        private static void Manager_ConnectionFailedToClose(object sender, ConnectionFailedToCloseEventArgs e)
        {
            Console.WriteLine(e.ErrorMessage);
        }

        private static void Manager_ConnectionClosedSuccessfully(object sender, EventArgs e)
        {
            Console.WriteLine("Updated successfully");
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
