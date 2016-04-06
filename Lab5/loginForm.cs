using System;
using System.Windows.Forms;
using Login;
//Login DLL namespaces
using UserManagement.Security;

namespace Lab5
{
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
        }

        //Try to login
        private void loginBtn_Click(object sender, EventArgs e)
        {
            //First check if any of the fields is not empty
            if (usernameField.Text == string.Empty || passwordField.Text == string.Empty || roleField.Text == string.Empty)
            {
                MessageBox.Show("Make sure to fill every field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            //Store values into variables to avoid any SQL Injection
            string un = usernameField.Text;
            string pw = passwordField.Text;
            enums.Roles r = (enums.Roles)Enum.Parse(typeof(enums.Roles), roleField.Text);                                     //Cast the role text to a Roles object

            //Create a secure credentials object
            LoginCredentials credentials = new LoginCredentials(un, pw, r);                                 //Needs Login.Security namespace            

            //Login by sending the credentials, and store the returning value as an enum object
            enums.LoginCases res = UserManagement.Login.login(credentials);

            
            //Switch through the result
            switch (res)
            {
                case enums.LoginCases.LoggedInAsAdmin:
                    //Login as an admin
                    AdminPage ap = new AdminPage();
                    ap.Show();
                    this.Hide();
                    break;
                case enums.LoginCases.LoggedInAsUser:
                    //Login as a normal user
                    WelcomePage wp = new WelcomePage();
                    wp.Show();
                    this.Hide();
                    break;
                case enums.LoginCases.WrongPasswordOrUsername:
                    //Display error message, indicating username or password were wrong.
                    MessageBox.Show("Wrong username or password.\nPlease try again.","Invalid credentials",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    break;
                case enums.LoginCases.UnkownRole:
                    //Display error message indicating the entered role is unknown
                    MessageBox.Show("You have chosen an unkown role.\nPlease try again.", "Invalid role", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                default:
                    //Display err message indicating an unkown error has occured
                    MessageBox.Show("An known error have occured.\nPlease try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }            
        }

        //End the program
        private void exitBtn_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);                                                                            //Terminate the program
        }

        private void loginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
