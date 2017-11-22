using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsLogin
{
    public partial class Login : Form
    {
        public static string UsernameEMail { get; set; }
        public static string Password { get; set; }
        public Login()
        {
            InitializeComponent();
        }

        private void RegisterLabel7_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Hide();
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            UsernameEMail = UsernameTextBox1.Text.Trim();
            Password = PasswordTextBox2.Text;

            string connectString = null;
            string sql = null;

            connectString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kenne\Source\Repos\LoginWindows\WindowsLogin\WindowsLogin\WLoginDatabase1.mdf;Integrated Security=True";
            sql = @"SELECT COUNT(*) FROM LoginTable
                    WHERE
                        Email = '"+ UsernameEMail + "' AND Password = '"+ Password + "'";

            DataTable DT = new DataTable();

            using (SqlConnection connect = new SqlConnection(connectString))
            using (SqlDataAdapter sda = new SqlDataAdapter(sql, connect))
            {
                sda.Fill(DT);

                if (DT.Rows[0][0].ToString() == "1")
                {
                    MainPage mainPage = new MainPage();
                    mainPage.Show();
                    this.Hide();

                }
                else
                {
                    MessageBox.Show("Invalid username or password");
                }

            }

        }

        private void UsernameTextBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void PasswordTextBox2_TextChanged(object sender, EventArgs e)
        {
            //posibility to HASH the password

        }
    }
}
