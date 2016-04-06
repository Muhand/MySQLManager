using System;
using System.Windows.Forms;
using UserManagement;
using Login;
namespace Lab5
{
    public partial class AdminPage : Form
    {
        public AdminPage()
        {
            InitializeComponent();
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if(tabControl1.SelectedTab == tabPage2)
            {
                //Create a new object of the database manager
                MySQLManagement manager = new MySQLManagement();

                //Retrieve the information and show it in the datagrid view
                dataGridView1.DataSource = manager.getData("*", "users").Tables["users"];
            }
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            usernameField.Text = string.Empty;
            passwordField.Text = string.Empty;
            roleField.SelectedIndex = 0;
        }

        private void createBtn_Click(object sender, EventArgs e)
        {
            //Create the new fields
            NewField username =  new NewField("username", usernameField.Text, enums.Tags.TextField);
            NewField password = new NewField("password", passwordField.Text, enums.Tags.Password);
            NewField role = new NewField("role", roleField.Text, enums.Tags.TextField);

            //Make a new object of the sign up and pass the fields
            SignUp s = new SignUp("users",username, password, role);

            //Login and check back the results
            switch (s.process())
            {
                case enums.SignUP.Success:
                    MessageBox.Show("A new user have been added successfully.\n", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case enums.SignUP.Duplicate:
                    MessageBox.Show("Sorry this user already exists in the database.\nPlease try another data.\n", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case enums.SignUP.UknownError:
                    MessageBox.Show("An error have occured while adding a new user.\nPlease try again.\n", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                default:
                    break;
            }
               
        }
    }
}
