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
    public partial class MainPage : Form
    {
        string username = Login.UsernameEMail;
        string password = Login.Password;

        public MainPage()
        {
            InitializeComponent();
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            string connectString = null;
            string sql = null;

            connectString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kenne\Source\Repos\LoginWindows\WindowsLogin\WindowsLogin\WLoginDatabase1.mdf;Integrated Security=True";
            sql = @"SELECT * FROM LoginTable
                    WHERE
                        Email = '" + username + "' AND Password = '" + password + "'";

            DataTable DT = new DataTable();

            using (SqlConnection connect = new SqlConnection(connectString))
            using (SqlDataAdapter sda = new SqlDataAdapter(sql, connect))
            {
                sda.Fill(DT);

                NameLabel.Text = $"{DT.Rows[0][1].ToString()} {DT.Rows[0][2].ToString()}";
                EmailLabel2.Text = $"{DT.Rows[0][3].ToString()}";
                CityLabel3.Text = $"{DT.Rows[0][5].ToString()}";

            }
        }
    }
}
