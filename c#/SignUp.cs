using System.Text;
using UserManagement;
using MySql.Data.MySqlClient;
namespace Login
{
    public class SignUp
    {
        //Prepare to build the sign up query by creating the strings from the passed fields and their values
        private StringBuilder fieldsString;
        private StringBuilder fieldsValues;
        private string Query;
        MySQLManagement databaseManager;
        public SignUp(string tablename,params NewField[] fields)
        {
            databaseManager = new MySQLManagement();
            //New StringBuilder
            fieldsString = new StringBuilder();
            fieldsValues = new StringBuilder();

            //Start building the fieldsString and fieldsValues by opening their parenthesis
            fieldsString.Append("(");
            fieldsValues.Append("(");

            //Loop through all the new fields
            for (int i = 0; i < fields.Length; i++)
            {
                //If any of them holds a password then encrypt it and prepare it.
                if(fields[i].tag == enums.Tags.Password)
                    fields[i].fieldValue = UserManagement.Security.Encryption.MD5Encryption(fields[i].fieldValue);

                //Now start building parts of the query
                //This condition is usefull because we don't want to add comma for the last field
                if (i != fields.Length - 1)
                {
                    fieldsString.Append(fields[i].fieldName + ", ");
                    fieldsValues.Append("'"+fields[i].fieldValue + "', ");
                }
                else
                {
                    fieldsString.Append(fields[i].fieldName);
                    fieldsValues.Append("'"+fields[i].fieldValue+"'");
                }
            }

            fieldsString.Append(")");
            fieldsValues.Append(")");

            //Now finish up the query
            Query = "INSERT INTO "+databaseManager.getDatabaseName()+"."+tablename+" "+fieldsString+" VALUES "+fieldsValues+";";
        }

        public enums.SignUP process()
        {
            enums.SignUP res;
            try
            {
                //Try to connect to the database
                databaseManager.connect();

                //Create a new commands
                MySqlCommand cmd = new MySqlCommand(Query,databaseManager.getConnection());

                //Execute the command
                if (cmd.ExecuteNonQuery() > 0)                                              //If the result was more than 0, it means we were successful
                    res = enums.SignUP.Success;
                else
                    res = enums.SignUP.UknownError;

            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)                                                     //Error code "1062" means a duplicate (Found in the MYSQL documentation), if duplicate is found 
                    res = enums.SignUP.Duplicate;                                          //Then return it as a result.
                else
                    res = enums.SignUP.UknownError;                                        //Otherwise return an unknown error
            }
            finally
            {
                //No matter what, disconnect from the server
                databaseManager.disconnect();
            }

            return res;
        }
    }
}
