//This class manages the MySQL server, in all general aspects. (e.g. connecting, disconnecting, preparing connection)
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace UserManagement
{
    public class MySQLManagement
    {
        //Private data members
        private string connectionUsername;
        private string connectionPassword;
        private string databaseName;
        private string serverIP;
        private string serverPort;
        private string connectionString;
        private MySqlConnection cn;

        //Default Constructor
        public MySQLManagement()
        {
            connectionUsername = "Muhand";
            connectionPassword = "test";
            databaseName = "lab5";
            serverIP = "127.0.0.1";
            serverPort = "3306";
            connectionString = "Server = " + serverIP + "; uid = " + connectionUsername + "; Password = " + connectionPassword + "; Database = " + databaseName + "; Port = " + serverPort + ";";
            cn = new MySqlConnection(connectionString);
        }

        /// <summary>
        /// This function will return a boolean value, indicating a succesful connection or a failure.
        /// </summary>
        /// <returns></returns>
        protected internal bool connect()
        {
            //Check connection status
            if (cn.State == System.Data.ConnectionState.Open)
            {
                //If open 
                if (disconnect())                                               //Try to disconnect,
                {
                    //If disconnected
                    cn.Open();                                                  //Open the connection
                    return true;
                }
                return false;
            }
            //If connection was close then just open it
            cn.Open();

            return true;
        }

        /// <summary>
        /// This function will return a boolean value, indicating a succesful disconnect or a failure.
        /// </summary>
        /// <returns></returns>
        protected internal bool disconnect()
        {
            //Check if connection is open
            if (cn.State == System.Data.ConnectionState.Open)
            {
                //If so then close it
                cn.Close();
                return true;
            }

            return false;
        }

        /// <summary>
        /// This function will return a data set containg the data retrieved from the database.
        /// </summary>
        /// <param name="columns">The names of all the columns to be retrieved seperated with a comma, or use '*' to retrieve all the columns.</param>
        /// <param name="table">The name of the table to retreive the information from.</param>
        /// <returns></returns>
        public DataSet getData(string columns, string table)
        {
            DataSet data = new DataSet();

            //Build the read query
            string Query = "Select " + columns + " FROM " + table;

            try
            {
                //Connect to the database
                connect();

                //Create an adapter
                MySqlDataAdapter reader = new MySqlDataAdapter(Query,cn);

                //Fill the dataset with the specific table
                reader.Fill(data, table);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //Finally disconnect regardless of the TRY CATCH result
                disconnect();
            }

            return data;
        }

        protected internal MySqlConnection getConnection()
        {
            return cn;
        }
        protected internal string getDatabaseName()
        {
            return databaseName;
        }

        public override string ToString()
        {
            return(String.Format("Server IP = {0}\nServer Port = {1}\nConnection Status = {2}",serverIP,serverPort,cn.State.ToString()));
        }
    }
}
