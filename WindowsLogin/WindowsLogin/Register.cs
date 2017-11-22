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
using System.Text.RegularExpressions;

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
            bool isEmpty = IsEmpty();
            bool validEmail = IsValidEmail(EmailTextBox3);
            bool matchPasswordVerify = CheckPasswordVerify(PasswordTextBox6, VerifyTextBox7);
            bool passwordnotnull = PasswordNotNull(PasswordTextBox6);


            if (validEmail && matchPasswordVerify && isEmpty && passwordnotnull)
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
                    try //this will check for uniqueness of the email..
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



                        Login login = new Login();
                        login.Show();
                        this.Close();
                    }
                    catch (Exception)
                    {
                        LabelEmailError.Visible = true;
                        LabelEmailError.Text = "The email address is in use!";
                    }

                }

            }



        }

        private bool IsEmpty()
        {
            if (this.Controls.OfType<TextBox>().Any(t => string.IsNullOrEmpty(t.Text)))
            {
                LabelFilloutInformation.Visible = true;
                LabelFilloutInformation.Text = "Fill out all information..";
                return false;
            }
            return true;
        }
        private bool IsValidEmail(TextBox textBox)
        {
            bool check = false;
            try
            {
                Regex rx = new Regex(
            @"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");
                check = rx.IsMatch(textBox.Text);
            }
            catch (FormatException)
            {
                check = false;
            }

            if (check == false)
            {
                LabelEmailError.Visible = true;
                LabelEmailError.Text = "Failure.. ex. JohnDoe@Unknown.com";
                return false;
            }
            else
                return true;
        }
        private bool PasswordNotNull(TextBox textBox)
        {
            if (textBox.Text.Length < 8)
            {
                LabelPasswordError.Visible = true;
                LabelPasswordError.Text = "Password must be longer that 8 characters long";
                return false;
            }
            else
                return true;
        }
        private bool CheckPasswordVerify(TextBox password, TextBoxBase verify)
        {
            if (password.Text != verify.Text)
            {
                LabelPasswordError.Visible = true;
                LabelPasswordError.Text = "The password and verify password does not match!";
                return false;
            }
            else
            {
                return true;
            }

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
        private void EmailTextBox3_TextChanged(object sender, EventArgs e)
        {
            LabelEmailError.Visible = false;
        }
        private void PasswordTextBox6_TextChanged(object sender, EventArgs e)
        {
            LabelPasswordError.Visible = false;
        }
        private void VerifyTextBox7_TextChanged(object sender, EventArgs e)
        {
            LabelPasswordError.Visible = false;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }
    }
}
