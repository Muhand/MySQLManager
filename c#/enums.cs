//This class will hold the different types of enums
namespace Login
{
    public class enums
    {
        public enum LoginCases
        {
            LoggedInAsAdmin,
            LoggedInAsUser,
            WrongPasswordOrUsername,
            UnkownRole,
            UnknownError
        }
        public enum SignUP
        {
            Success,
            Duplicate,
            UknownError
        }
        public enum Roles
        {
            Admin,
            User
        }
        public enum Tags
        {
            Password,
            TextField
        }
    }
}
