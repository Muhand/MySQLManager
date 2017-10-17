using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySQLManager.EventArguments
{
    public class ConnectionFailedToOpenEventArgs : EventArgs
    {
        public string ErrorMessage { get; private set; }
        private MySqlException error;

        public ConnectionFailedToOpenEventArgs(MySqlException ex)
        {
            this.error = ex;
            switch (this.error.Number)
            {
                case 0:
                    this.ErrorMessage = "Cannot connect to the server, Contact administrator";
                    break;
                case 1045:
                    this.ErrorMessage = "Invalid username/password, please try again";
                    break;
                default:
                    this.ErrorMessage = this.error.Message;
                    break;
            }
        }
    }
}
