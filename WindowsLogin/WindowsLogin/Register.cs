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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void StoringRegistrationToDatabase()
        {
            string connectionString = null;
            string sql = null;

            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kenne\Source\Repos\LoginWindows\WindowsLogin\WindowsLogin\WLoginDatabase1.mdf;Integrated Security=True";
            sql = @"INSERT INTO LoginTable
                        (Firstname, Lastname, Email, Country, City, Password, Verify)
                    VALUES
                        (@firstname, @lastname, @email, @country, @city, @password, @verify)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                connection.Open();
                cmd.Parameters.AddWithValue("@firstname", FirstnameTextBox1.Text);
                cmd.Parameters.AddWithValue("@lastname", LastnameTextBox2.Text);
                cmd.Parameters.AddWithValue("@email", EmailTextBox3.Text);
                cmd.Parameters.AddWithValue("@country", CountryTextBox4.Text);
                cmd.Parameters.AddWithValue("@city", CityTextBox5.Text);
                cmd.Parameters.AddWithValue("@password", PasswordTextBox6.Text);
                cmd.Parameters.AddWithValue("@verify", VerifyTextBox7.Text);
                cmd.ExecuteNonQuery();
            }


            Login login = new Login();
            this.Close();
        }


        private bool checkPasswordVerify(TextBox textBox, TextBoxBase textBox1)
        {
            if (textBox.Text != textBox1.Text)
            {
                MessageBox.Show("The password and verify password does not match!");
                return false;
            }
            else
                return true;
        }



        //----------------- Click ----------------------------------------------
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            StoringRegistrationToDatabase();
        }
        private void RegisterLabel11_Click(object sender, EventArgs e)
        {
            StoringRegistrationToDatabase();
        }
    }
}
