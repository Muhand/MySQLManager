//This class is responsible on logging in, if loging in was supported in the app
using System;
using MySql.Data.MySqlClient;
using Login;
namespace UserManagement
{
    public class Login
    {
        public static enums.LoginCases login(Security.LoginCredentials creds)
        {
            enums.LoginCases res;                                                                   //Prepare an enum value to return it at the end

            MySQLManagement databaseManager = new MySQLManagement();                                //Create a new instance of the manager, since we will use it for connecting, etc...

            MySqlCommand cmd = new MySqlCommand();                                                  //Create a new command
            try
            {
                //Try to first connect to the server
                databaseManager.connect();

                //Build the command to fetch the database
                cmd.CommandText = "select count(*) from users where username = '" + creds.username+"'and password = '" + creds.password+"' and role ='" + creds.role+"'";
                cmd.Connection = databaseManager.getConnection();

                //Look for the value
                int value = int.Parse(cmd.ExecuteScalar().ToString());

                //If the value is 1, then it means the user exist
                if (value == 1)                                                                     //If user exist
                {
                    //Change the result to different result, based on the role
                    if (creds.role == enums.Roles.Admin)
                        res = enums.LoginCases.LoggedInAsAdmin;
                    else if (creds.role == enums.Roles.User)
                        res = enums.LoginCases.LoggedInAsUser;
                    else
                        res = enums.LoginCases.UnkownRole;
                }
                else
                    res = enums.LoginCases.WrongPasswordOrUsername;
            }
            catch (Exception)
            {
                res = enums.LoginCases.UnknownError;
                throw;
            }
            finally
            {
                //Disconnect regardless
                databaseManager.disconnect();
            }

            return res;                                                                             //Return the result
        }
    }
}
