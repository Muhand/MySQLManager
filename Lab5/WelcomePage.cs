using System;
using System.Windows.Forms;

namespace Lab5
{
    public partial class WelcomePage : Form
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        private void WelcomePage_Load(object sender, EventArgs e)
        {
            //Center the panel
            panel1.Left = (this.Width - panel1.Width) / 2;
            panel1.Top = (this.Height - panel1.Height) / 2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loginForm lg = new loginForm();
            lg.Show();
            this.Close();
        }
    }
}
