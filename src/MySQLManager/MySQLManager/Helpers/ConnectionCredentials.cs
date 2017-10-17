using System;
using System.Collections.Generic;
using System.Text;

namespace MySQLManager.Helpers
{
    public struct ConnectionCredentials
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        /// <param name="database"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public ConnectionCredentials(string server, string database, string username, string password)
        {
            this.Server = server;
            this.Database = database;
            this.Username = username;
            this.Password = password;
        }
    }
}
