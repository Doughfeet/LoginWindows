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
            bool contains = false;
            string Email = null;
            string Password = null;
            string connectString = null;
            string sql = null;

            connectString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kenne\Source\Repos\LoginWindows\WindowsLogin\WindowsLogin\WLoginDatabase1.mdf;Integrated Security=True";
            sql = @"SELECT COUNT(*) FROM LoginTable
                    WHERE
                        Email = '"+ UsernameTextBox1.Text.Trim() +"' AND Password = '"+ PasswordTextBox2.Text +"'";

            DataTable DT = new DataTable();

            using (SqlConnection connect = new SqlConnection(connectString))
            using (SqlDataAdapter sda = new SqlDataAdapter(sql, connect))
            {
                sda.Fill(DT);

                if (DT.Rows[0][0].ToString() == "1")
                {
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
            EmailExistsLabel7.Visible = false;
        }
    }
}
