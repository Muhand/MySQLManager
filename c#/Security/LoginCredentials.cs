//This class is used so we hold the login credentials securly after encrypting the data, then send this obejct to the login class and process the login
using Login;

namespace UserManagement.Security
{
    public class LoginCredentials
    {
        //Private fields
        public string username
        {
            set;
            get;
        }

        public string password
        {
            set;
            get;
        }
        public enums.Roles role
        {
            set;
            get;
        }

        /// <summary>
        /// This obejcts stores user credentials
        /// </summary>
        /// <param name="un"></param>
        /// <param name="pw"></param>
        /// <param name="r"></param>
        public LoginCredentials(string un, string pw, enums.Roles r)
        {
            username = un;
            password = Encryption.MD5Encryption(pw);
            role = r;
        }

        
    }
}
