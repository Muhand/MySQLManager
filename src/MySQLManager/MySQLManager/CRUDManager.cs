using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using MySQLManager.Helpers;
using MySQLManager.EventArguments;
using System.Linq;
using MySQLManager.Enums;
using System.ComponentModel;

namespace MySQLManager
{
    public sealed class CRUDManager : IDisposable
    {
        #region Properties

        #endregion

        #region Events
        public event EventHandler ConnectionOpenedSuccessfully;
        public event EventHandler<ConnectionFailedToOpenEventArgs> ConnectionFailedToOpen;
        public event EventHandler ConnectionClosedSuccessfully;
        public event EventHandler<ConnectionFailedToCloseEventArgs> ConnectionFailedToClose;
        public event EventHandler CreatedSuccessfully;
        public event EventHandler<FailedToCreateEventArgs> FailedToCreate;
        public event EventHandler ReadSuccessfully;
        public event EventHandler UpdatedSuccessfully;
        public event EventHandler<FailedToUpdateEventArgs> FailedToUpdate;
        public event EventHandler DeletedSuccessfully;
        public event EventHandler<FailedToDeleteEventArgs> FailedToDelete;
        #endregion

        #region Global Variables
        private MySqlConnection connection;
        private string server;
        private string database;
        private string username;
        private string password;

        private bool IsDisposed;
        #endregion

        #region Constructor(s)
        public CRUDManager(ConnectionCredentials credentials)
        {
            this.server = credentials.Server;
            this.database = credentials.Database;
            this.username = credentials.Username;
            this.password = credentials.Password;

            Initialize();
        }

        #endregion

        #region Initializer
        private void Initialize()
        {
            string connectionString = String.Format("SERVER={0};DATABASE={1};UID={2};PASSWORD={3};",this.server,this.database,this.username,this.password);
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
                tempCols += String.Format("{0},",newRow.Column);
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


        public void Read()
        {

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

        public void Delete(string tablename, AssignmentList conditions)
        {
            Field[] conds = conditions.Fields;
            Delete(tablename, conds);
        }


        #endregion

        #region Clean Up
        public void Dispose()
        {
            checkDisposed();

            //As long as the object is still not disposed
            if(!this.IsDisposed)
            {

            }
        }
        #endregion
    }
}