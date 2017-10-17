using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySQLManager.EventArguments
{
    public class ConnectionFailedToCloseEventArgs : EventArgs
    {
        public string ErrorMessage { get; private set; }
        private MySqlException error;

        public ConnectionFailedToCloseEventArgs(MySqlException ex)
        {
            this.error = ex;
            switch (this.error.Number)
            {
                default:
                    this.ErrorMessage = this.error.Message;
                    break;
            }
        }
    }
}
