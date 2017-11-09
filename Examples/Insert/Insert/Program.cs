using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySQLManager;
using MySQLManager.Helpers;
using MySQLManager.EventArguments;

namespace Insert
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionCredentials credentials = new ConnectionCredentials
            {
                Server = "www.muhandjumah.com",
                Database = "muhand5_ls_static",
                Username = "muhand5_ls_statk",
                Password = "=Cs_U!JtHFbt"
            };

            CRUDManager manager = new CRUDManager(credentials);
            manager.ConnectionOpenedSuccessfully += Manager_ConnectionOpenedSuccessfully; ;
            manager.ConnectionFailedToOpen += Manager_ConnectionFailedToOpen; ;
            manager.ConnectionClosedSuccessfully += Manager_ConnectionClosedSuccessfully;
            manager.ConnectionFailedToClose += Manager_ConnectionFailedToClose;
            manager.CreatedSuccessfully += Manager_CreatedSuccessfully;
            manager.FailedToCreate += Manager_FailedToCreate;

            Console.WriteLine("Name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Age: ");
            int age = Convert.ToInt32(Console.ReadLine());

            Columns columns = new Columns("name", "age");
            Values values = new Values(name, age.ToString());
            manager.Create("info", columns, values);


            Console.WriteLine("Pres any key to continue...");
            Console.ReadKey();

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
