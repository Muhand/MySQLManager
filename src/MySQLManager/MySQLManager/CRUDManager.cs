/**************************************************/
/*                  Muhand Jumah
 *               CRUDManager V1.0.0
 * This library will allow users to manager their database easily
 * The user will be able to do CRUD (Create, Read, Update and Delete) operations
 * with ease. The user will have to make 1 function call and thats it.
 * Users can also subscribe to events to get notifications back.
 * ************************************************/
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using MySQLManager.Helpers;
using MySQLManager.EventArguments;
using System.Linq;
using MySQLManager.Enums;

namespace MySQLManager
{
    public sealed class CRUDManager : IDisposable
    {
        #region Properties
        public bool IsDisposed { get; private set; }
        #endregion

        #region Events
        public event EventHandler ConnectionOpenedSuccessfully;
        public event EventHandler<ConnectionFailedToOpenEventArgs> ConnectionFailedToOpen;
        public event EventHandler ConnectionClosedSuccessfully;
        public event EventHandler<ConnectionFailedToCloseEventArgs> ConnectionFailedToClose;
        public event EventHandler CreatedSuccessfully;
        public event EventHandler<FailedToCreateEventArgs> FailedToCreate;
        public event EventHandler ReadSuccessfully;
        public event EventHandler<FailedToReadEventArgs> FailedToRead;
        public event EventHandler UpdatedSuccessfully;
        public event EventHandler<FailedToUpdateEventArgs> FailedToUpdate;
        public event EventHandler DeletedSuccessfully;
        public event EventHandler<FailedToDeleteEventArgs> FailedToDelete;
        public event EventHandler CustomQueryExecutedSuccessfully;
        public event EventHandler<FailedToExecuteCustomQueryEventArgs> FailedToExecuteCustomQuery;
        #endregion

        #region Global Variables
        private MySqlConnection connection;
        private string server;
        private string database;
        private string username;
        private string password;
        private bool sslRequired;
        #endregion

        #region Constructor(s)
        public CRUDManager(ConnectionCredentials credentials)
        {
            this.server = credentials.Server;
            this.database = credentials.Database;
            this.username = credentials.Username;
            this.password = credentials.Password;
            this.sslRequired = false;

            Initialize();
        }

        public CRUDManager(ConnectionCredentials credentials, bool IsSSLRequired)
        {
            this.server = credentials.Server;
            this.database = credentials.Database;
            this.username = credentials.Username;
            this.password = credentials.Password;
            this.sslRequired = IsSSLRequired;

            Initialize();
        }

        #endregion

        #region Initializer
        private void Initialize()
        {
            string connectionString;

            if(!this.sslRequired)
                connectionString = String.Format(@"SERVER={0};DATABASE={1};UID={2};PASSWORD={3};SslMode=none", this.server,this.database,this.username,this.password);
            else
                connectionString = String.Format(@"SERVER={0};DATABASE={1};UID={2};PASSWORD={3};SslMode=Required", this.server, this.database, this.username, this.password);

            this.connection = new MySqlConnection(connectionString);
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Open the connection properly
        /// </summary>
        /// <returns></returns>
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                RaiseAnEvent(ConnectionOpenedSuccessfully, EventArgs.Empty);
                return true;
            }
            catch (MySqlException ex)
            {
                RaiseAnEvent(ConnectionFailedToOpen, new ConnectionFailedToOpenEventArgs(ex));
                return false;   
            }
        }

        /// <summary>
        /// Close the connection to the database properly
        /// </summary>
        /// <returns></returns>
        private bool CloseConnection()
        {
            try
            {
                this.connection.Close();
                RaiseAnEvent(ConnectionClosedSuccessfully, EventArgs.Empty);
                return true;
            }
            catch (MySqlException ex)
            {
                RaiseAnEvent(ConnectionFailedToClose, new ConnectionFailedToCloseEventArgs(ex));
                return false;
            }
        }

        #region Raising an event
        /// <summary>
        /// Raise an event
        /// </summary>
        /// <param name="eventHandler">The event you would like to raise</param>
        /// <param name="args">The event arguments</param>
        private void RaiseAnEvent(EventHandler eventHandler, EventArgs args)
        {
            //If event is not null then raise it
            eventHandler?.Invoke(this, args);
        }

        /// <summary>
        /// Raise an event
        /// </summary>
        /// <param name="eventHandler">The event you would like to raise</param>
        /// <param name="args">The event arguments</param>
        private void RaiseAnEvent(EventHandler<EventArgs> eventHandler, EventArgs args)
        {
            //If event is not null then raise it
            eventHandler?.Invoke(this, args);
        }

        /// <summary>
        /// Raise an event
        /// </summary>
        /// <param name="eventHandler">The event you would like to raise</param>
        /// <param name="args">The event arguments</param>
        private void RaiseAnEvent(EventHandler<ConnectionFailedToCloseEventArgs> eventHandler, ConnectionFailedToCloseEventArgs args)
        {
            //If event is not null then raise it
            eventHandler?.Invoke(this, args);
        }

        /// <summary>
        /// Raise an event
        /// </summary>
        /// <param name="eventHandler">The event you would like to raise</param>
        /// <param name="args">The event arguments</param>
        private void RaiseAnEvent(EventHandler<ConnectionFailedToOpenEventArgs> eventHandler, ConnectionFailedToOpenEventArgs args)
        {
            //If event is not null then raise it
            eventHandler?.Invoke(this, args);
        }

        // <summary>
        /// Raise an event
        /// </summary>
        /// <param name="eventHandler">The event you would like to raise</param>
        /// <param name="args">The event arguments</param>
        private void RaiseAnEvent(EventHandler<FailedToCreateEventArgs> eventHandler, FailedToCreateEventArgs args)
        {
            //If event is not null then raise it
            eventHandler?.Invoke(this, args);
        }

        // <summary>
        /// Raise an event
        /// </summary>
        /// <param name="eventHandler">The event you would like to raise</param>
        /// <param name="args">The event arguments</param>
        private void RaiseAnEvent(EventHandler<FailedToUpdateEventArgs> eventHandler, FailedToUpdateEventArgs args)
        {
            //If event is not null then raise it
            eventHandler?.Invoke(this, args);
        }

        // <summary>
        /// Raise an event
        /// </summary>
        /// <param name="eventHandler">The event you would like to raise</param>
        /// <param name="args">The event arguments</param>
        private void RaiseAnEvent(EventHandler<FailedToDeleteEventArgs> eventHandler, FailedToDeleteEventArgs args)
        {
            //If event is not null then raise it
            eventHandler?.Invoke(this, args);
        }

        // <summary>
        /// Raise an event
        /// </summary>
        /// <param name="eventHandler">The event you would like to raise</param>
        /// <param name="args">The event arguments</param>
        private void RaiseAnEvent(EventHandler<FailedToReadEventArgs> eventHandler, FailedToReadEventArgs args)
        {
            //If event is not null then raise it
            eventHandler?.Invoke(this, args);
        }

        // <summary>
        /// Raise an event
        /// </summary>
        /// <param name="eventHandler">The event you would like to raise</param>
        /// <param name="args">The event arguments</param>
        private void RaiseAnEvent(EventHandler<FailedToExecuteCustomQueryEventArgs> eventHandler, FailedToExecuteCustomQueryEventArgs args)
        {
            //If event is not null then raise it
            eventHandler?.Invoke(this, args);
        }


        /// <summary>
        /// Raise an event
        /// </summary>
        /// <param name="sender">The object raising the event</param>
        /// <param name="eventHandler">The event you would like to raise</param>
        /// <param name="args">The event you would like to raise</param>
        private void RaiseAnEvent(object sender, EventHandler eventHandler, EventArgs args)
        {
            eventHandler?.Invoke(sender, args);
        }

        #endregion

        private void checkDisposed()
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException("this");
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Create a new row in the database
        /// </summary>
        /// <param name="tablename">The table name you would like to insert new rows into</param>
        /// <param name="columns">The columns you would like to insert into</param>
        /// <param name="values">The values to be inserted</param>
        public void Create(string tablename,Columns columns, Values values)
        {
            //If number of columns doesn't equal to number of values then raise an event of failed to create due not matching 
            if(columns.length != values.length)
            {
                RaiseAnEvent(FailedToCreate,new FailedToCreateEventArgs(Enums.FailedToCreate.IncorrectLength));
                return;
            }
            
            //If the length is equal to 0 meaning there is nothing to insert then raise an event and return nothing
            if(columns.length == 0 || values.length == 0)
            {
                RaiseAnEvent(FailedToCreate, new FailedToCreateEventArgs(Enums.FailedToCreate.LengthIsZero));
                return;
            }

            //Otherwise, loop through all columns and values and append them
            string tempCols = "(";
            string tempVals = "(";

            //Since columns.length = values.length then use 1 loop using ZIP in system.linq
            var newRows = columns.columns.Zip(values.values, (n,w)=>new { Column = n, Value= w });
            foreach (var newRow in newRows)
            {
                tempCols += String.Format("`{0}`,",newRow.Column);
                tempVals += string.Format("'{0}',",newRow.Value);
            }

            //Do a little bit of cleaning from the previous loop
            /* 1. Remove the last comma which will be added by the loop 
             * 2. Append parenthesis at the end of each
             */
            string cols = tempCols.Remove(tempCols.Length - 1, 1) + ")";
            string vals = tempVals.Remove(tempVals.Length - 1, 1) + ")";

            //Create our insert query
            string query = string.Format("INSERT INTO {0} {1} VALUES {2}",tablename,cols,vals);

            //Open our connection
            if(this.OpenConnection() == true)
            {
                //If the connection was open then proceed with inserting

                try
                {
                    //Create a command assign the query and connection to it
                    MySqlCommand cmd = new MySqlCommand(query, this.connection);

                    //Execute the command
                    cmd.ExecuteNonQuery();

                    //Raise an event that creation was successful
                    RaiseAnEvent(CreatedSuccessfully, EventArgs.Empty);
                }
                catch (MySqlException ex)
                {
                    RaiseAnEvent(FailedToCreate, new FailedToCreateEventArgs(Enums.FailedToCreate.MySQLException,ex));
                }
                finally
                {
                    //Close the connection
                    this.CloseConnection();
                }
            }
            else
            {
                //Otherwise raise an event and return
                RaiseAnEvent(FailedToCreate, new FailedToCreateEventArgs(Enums.FailedToCreate.ConnectionWasNotOpen));
                return;
            }

        }

        /// <summary>
        /// Read all records from table
        /// </summary>
        /// <param name="tablename">Table name</param>
        /// <returns></returns>
        public List<List<string>> Read(string tablename)
        {
            //Create this variable to avoid ambiguous calls
            string[] temp = null;
            return Read(tablename, temp);
        }

        /// <summary>
        /// Read specific columns from table
        /// </summary>
        /// <param name="tablename">Table name</param>
        /// <param name="colsNames">Columns names</param>
        /// <returns></returns>
        public List<List<string>> Read(string tablename, params string[] colsNames)
        {
            return (ExecuteRead(tablename, colsNames, null));
        }

        /// <summary>
        /// Read all records of a table based on conditions
        /// </summary>
        /// <param name="tablename">Table name</param>
        /// <param name="conditions">All conditions</param>
        /// <returns></returns>
        public List<List<string>> Read(string tablename, Field[] conditions)
        {
            return Read(tablename, conditions, null);
        }

        /// <summary>
        /// Read specific columns and cells using conditions
        /// </summary>
        /// <param name="tablename">Table name</param>
        /// <param name="conditions">All conditions</param>
        /// <param name="colsNames">Columns names</param>
        /// <returns>Returns a multidimensional list where it will contain rows->columns</returns>
        public List<List<string>> Read(string tablename, Field[] conditions, params string[] colsNames)
        {
            return (ExecuteRead(tablename, colsNames, conditions));
        }

        private List<List<string>> ExecuteRead(string tablename, string[] colsNames, Field[] conditions)
        {
            //If the table name is empty then raise an event and return null
            if (tablename == null || tablename == "")
            {
                RaiseAnEvent(FailedToRead, new FailedToReadEventArgs(Enums.FailedToRead.EmptyTableName));
                return null;
            }

            //Local variables
            bool readingAll = false;
            string query = "";
            //Create a 2D list to hold our rows and columns
            List<List<string>> res = new List<List<string>>();

            //If the number of Columns is 0 or null then set query to start reading all columns
            if (colsNames == null || colsNames.Length == 0)
                readingAll = true;

            //If we are reading all the columns then set a query to read all
            if (readingAll)
                query = String.Format("SELECT * FROM {0}", tablename);
            else
            {
                //Otherwise start building up the query by Columns
                string tempCols = "";

                foreach (var item in colsNames)
                {
                    //Append the columns together
                    tempCols += String.Format("{0},", item);
                }

                //Filter tempCols by removing the last character which would be "," because of the previous loop
                string cols = tempCols.Remove(tempCols.Length - 1, 1) + "";

                //Build up the query
                query = String.Format("SELECT {0} FROM {1}", cols, tablename);
            }

            //Check if we have any conditions then append them to the query
            if(conditions != null && conditions.Length > 0)
            {
                //Make local variable to hold conditions
                string tempConds = "";

                //Append all conditions
                foreach (var item in conditions)
                    tempConds += String.Format("{0} AND",item.Formatted);

                //Format conditions
                //string conds = tempConds.Remove(tempConds.Length - 4, 1) + "";
                string conds = tempConds.Substring(0,tempConds.Length-4);

                //Append the conditions
                query += String.Format(" WHERE {0}", conds);
            }


            //Check if connection is open
            if (this.OpenConnection() == true)
            {
                try
                {
                    //Create command
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    //Create a data reader and execute the command
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    //This variable will count number of rows being read so far
                    int rowsCounter = 0;

                    while (dataReader.Read())
                    {
                        //Every time we read a new row, create a list to hold the columns of that row
                        res.Add(new List<string>());

                        //Loop through the number of columns
                        for (int i = 0; i < dataReader.FieldCount; i++)
                            //Add the columns into the row list
                            res[rowsCounter].Add(dataReader.GetString(i));

                        rowsCounter++;
                    }

                    //Close dataReader
                    dataReader.Close();

                    RaiseAnEvent(ReadSuccessfully, EventArgs.Empty);
                }
                catch (MySqlException ex)
                {
                    RaiseAnEvent(FailedToRead, new FailedToReadEventArgs(Enums.FailedToRead.MySQLException, ex));
                }
                finally
                {
                    this.CloseConnection();
                }

            }
            else
            {
                RaiseAnEvent(FailedToRead, new FailedToReadEventArgs(Enums.FailedToRead.ConnectionWasNotOpen));
                return null;
            }

            return res;
        }
        
        /// <summary>
        /// Update a table
        /// </summary>
        /// <param name="tablename">Table name</param>
        /// <param name="fields">The fields you would like to update</param>
        /// <param name="conditions">The conditions that must be met in order to update
        /// if length == 0 then the entire table will be updated
        /// </param>
        public void Update(string tablename, Field[] fields, Field[] conditions)
        {
            if(fields.Length == 0)
            {
                RaiseAnEvent(FailedToUpdate, new FailedToUpdateEventArgs(Enums.FailedToUpdate.LengthIsZero));
                return;
            }
            ////#if DEBUG
            //if(conditions.Length == 0)
            //{
            //    //If the conditions throw a warning in case the developer made a mistake
            //    #warning There are no conditions set, this will update the entire table, if this was intentional then ignore this warning.
            //}
            ////#endif
            //Some local variables to hold lengths in case we need them
            int fieldsLenth = fields.Length;
            int conditionsLength = conditions.Length;

            //Otherwise, loop through all columns and values and append them
            string tempFields = "";
            string tempConditions = "";

            //Loop through fields and append them together
            foreach (var field in fields)
                tempFields += String.Format("{0},", field.Formatted);

            //Only if we have conditions then append them otherwise update the entire table
            if (conditionsLength > 0)
            {
                foreach (var condition in conditions)
                {
                    if(condition.Formatted!= null && (condition.FieldName != null && condition.FieldName != "" && condition.FieldValue != null && condition.FieldValue != ""))
                        tempConditions += String.Format("{0},", condition.Formatted);
                }
            }

            //Do a little bit of cleaning from the previous loop
            /* 1. Remove the last comma which will be added by the loop 
             */
            string fie = tempFields.Remove(tempFields.Length - 1, 1) + "";

            //Create our Update query
            //First create it without the WHERE clause
            string query = string.Format("UPDATE {0} SET {1}", tablename, fie);

            //If we do have some conditions then append the WHERE clause and the conditions
            if (conditionsLength > 0)
            {
                if (tempConditions != "" && tempConditions != null)
                {
                    string cond = tempConditions.Remove(tempConditions.Length - 1, 1) + "";

                    if (cond != null || cond != "")
                    {
                        //Create our insert query
                        query += string.Format(" WHERE {0}", cond);
                    }
                }
            }


            //Open our connection
            if (this.OpenConnection() == true)
            {
                //If the connection was open then proceed with inserting

                try
                {
                    //Create a command assign the query and connection to it
                    MySqlCommand cmd = new MySqlCommand(query, this.connection);

                    //Execute the command
                    cmd.ExecuteNonQuery();

                    //Raise an event that creation was successful
                    RaiseAnEvent(UpdatedSuccessfully, EventArgs.Empty);
                }
                catch (MySqlException ex)
                {
                    RaiseAnEvent(FailedToUpdate, new FailedToUpdateEventArgs(Enums.FailedToUpdate.MySQLException, ex));
                }
                finally
                {
                    //Close the connection
                    this.CloseConnection();
                }
            }
            else
            {
                //Otherwise raise an event and return
                RaiseAnEvent(FailedToUpdate, new FailedToUpdateEventArgs(Enums.FailedToUpdate.ConnectionWasNotOpen));
                return;
            }
        }

        /// <summary>
        /// Update a table - This function is different because it accepts AssignmentList instead of Field[] for conditions and fields
        /// </summary>
        /// <param name="tablename">Table name</param>
        /// <param name="assignmentList">The fields you would like to update</param>
        /// <param name="conditions">The conditions that must be met in order to Update</param>
        public void Update(string tablename, AssignmentList assignmentList, AssignmentList conditions)
        {
            Field[] fields = assignmentList.Fields;
            Field[] conds = conditions.Fields;

            Update(tablename, fields, conds);
        }

        /// <summary>
        /// Delete row(s) from a table
        /// </summary>
        /// <param name="tablename">Table name</param>
        /// <param name="conditions">The conditions that must be met in order to delete
        /// Length should not be 0
        /// </param>
        public void Delete(string tablename, Field[] conditions)
        {
            if(conditions.Length == 0)
            {
                RaiseAnEvent(FailedToDelete, new FailedToDeleteEventArgs(Enums.FailedToDelete.LengthIsZero));
                return;
            }

            string tempCond = "";

            //Loop through all conditions and append them
            foreach (var cond in conditions)
            {
                tempCond += String.Format("{0},", cond.Formatted);
            }

            //Do a little bit of cleaning from the previous loop
            /* 1. Remove the last comma which will be added by the loop 
             * 2. Append parenthesis at the end of each
             */
            string conds = tempCond.Remove(tempCond.Length - 1, 1) + "";

            //Create our delete query
            string query = string.Format("DELETE FROM {0} WHERE {1}", tablename, conds);

            //Open our connection
            if (this.OpenConnection() == true)
            {
                //If the connection was open then proceed with deletion

                try
                {
                    //Create a command assign the query and connection to it
                    MySqlCommand cmd = new MySqlCommand(query, this.connection);

                    //Execute the command
                    cmd.ExecuteNonQuery();

                    //Raise an event that creation was successful
                    RaiseAnEvent(DeletedSuccessfully, EventArgs.Empty);
                }
                catch (MySqlException ex)
                {
                    RaiseAnEvent(FailedToDelete, new FailedToDeleteEventArgs(Enums.FailedToDelete.MySQLException, ex));
                }
                finally
                {
                    //Close the connection
                    this.CloseConnection();
                }
            }
            else
            {
                //Otherwise raise an event and return
                RaiseAnEvent(FailedToDelete, new FailedToDeleteEventArgs(Enums.FailedToDelete.ConnectionWasNotOpen));
                return;
            }
        }

        /// <summary>
        /// Delete row(s) from a table
        /// </summary>
        /// <param name="tablename">Table name</param>
        /// <param name="conditions">The conditions that must be met in order to delete; this function accepts AssignmentList as a paramer instead of Field[]</param>
        public void Delete(string tablename, AssignmentList conditions)
        {
            Field[] conds = conditions.Fields;
            Delete(tablename, conds);
        }

        /// <summary>
        /// Create a custom query and then execute it using this function
        /// By default this function will execute queries using ExecuteNonQuery();
        /// If you would like to use something else then please use ExecuteCustomQuery(query, ExecutionOptions) to use a different option
        /// </summary>
        /// <param name="query">The query</param>
        public void ExecuteCustomQuery(string query)
        {
            ExecuteCustomQuery(query, ExecutionOptions.ExecuteNonQuery);
        }

        /// <summary>
        /// Create a custom query and then execute it using this function with a specific ExecutionOption (e.g. ExecuteNonQuery, ExecuteScalar)
        /// </summary>
        /// <param name="query">The query</param>
        /// <param name="executionOption">The execution option</param>
        public void ExecuteCustomQuery(string query, ExecutionOptions executionOption)
        {
            if (query == null || query == "")
            {
                RaiseAnEvent(FailedToExecuteCustomQuery, new FailedToExecuteCustomQueryEventArgs(FailedToExecute.NullQuery));
                return;
            }

            if (OpenConnection() == true)
            {
                try
                {
                    //Create a command and assign the query and connection to it
                    MySqlCommand cmd = new MySqlCommand(query, this.connection);

                    switch (executionOption)
                    {
                        case ExecutionOptions.ExecuteNonQuery:
                            //Execute command
                            cmd.ExecuteNonQuery();
                            break;
                        case ExecutionOptions.ExecuteReader:
                            throw new Exception("If you are going to execute Reader then please add the 3rd paramter, where you have to provide a list to return results to.");
                            break;
                        case ExecutionOptions.ExecuteScalar:
                            //Execute command
                            cmd.ExecuteScalar();
                            break;
                        default:
                            RaiseAnEvent(FailedToExecuteCustomQuery, new FailedToExecuteCustomQueryEventArgs(FailedToExecute.UnknownExecutionOption));
                            break;
                    }

                    //Raise an event that execution was successful
                    RaiseAnEvent(CustomQueryExecutedSuccessfully, EventArgs.Empty);
                }
                catch (MySqlException ex)
                {
                    RaiseAnEvent(FailedToExecuteCustomQuery, new FailedToExecuteCustomQueryEventArgs(FailedToExecute.MySQLException, ex));
                }
                finally
                {
                    this.CloseConnection();
                }
            }
            else
            {
                RaiseAnEvent(FailedToExecuteCustomQuery, new FailedToExecuteCustomQueryEventArgs(FailedToExecute.ConnectionWasNotOpen));
                return;
            }
        }
        
        /// <summary>
        /// Create a custom query where you will be using ExecutionOptions.ExecuteRead, this is different because you must provide an output list
        /// </summary>
        /// <param name="query">The query</param>
        /// <param name="executionOption">The execution option</param>
        /// <param name="outputList">The list in which what is read will be returned to</param>
        public void ExecuteCustomQuery(string query, ExecutionOptions executionOption, out List<List<string>> outputList)
        {
            if(executionOption != ExecutionOptions.ExecuteReader)
            {
                throw new Exception("If you are going to use anything other than ExecutionOptions.ExecuteReader then please remove the last paramter");
                return;
            }

            //Create a 2D list to hold our rows and columns
            List<List<string>> res = new List<List<string>>();

            //Check if connection is open
            if (this.OpenConnection() == true)
            {
                try
                {
                    //Create command
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    //Create a data reader and execute the command
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    //This variable will count number of rows being read so far
                    int rowsCounter = 0;

                    while (dataReader.Read())
                    {
                        //Every time we read a new row, create a list to hold the columns of that row
                        res.Add(new List<string>());

                        //Loop through the number of columns
                        for (int i = 0; i < dataReader.FieldCount; i++)
                            //Add the columns into the row list
                            res[rowsCounter].Add(dataReader.GetString(i));

                        rowsCounter++;
                    }

                    //Close dataReader
                    dataReader.Close();

                    RaiseAnEvent(CustomQueryExecutedSuccessfully, EventArgs.Empty);
                }
                catch (MySqlException ex)
                {
                    RaiseAnEvent(FailedToExecuteCustomQuery, new FailedToExecuteCustomQueryEventArgs(Enums.FailedToExecute.MySQLException, ex));
                }
                finally
                {
                    this.CloseConnection();
                }
            }
            else
            {
                RaiseAnEvent(FailedToExecuteCustomQuery, new FailedToExecuteCustomQueryEventArgs(Enums.FailedToExecute.ConnectionWasNotOpen));
            }

            outputList = res;
        }
        #endregion

        #region Clean Up
        public void Dispose()
        {
            checkDisposed();

            //As long as the object is still not disposed
            if(!this.IsDisposed)
            {
                this.IsDisposed = true;
                this.connection.Dispose();
            }
        }
        #endregion
    }
}