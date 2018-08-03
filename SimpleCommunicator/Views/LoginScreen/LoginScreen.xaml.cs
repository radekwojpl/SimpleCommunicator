using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleCommunicator.Views.LoginScreen
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : UserControl
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=DESKTOP-D7LMP5L;Initial Catalog=LoginDB;Integrated Security=True");

            try
            {
                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                    string query = "SELECT COUNT(1) FROM tbUser WHERE UserName=@UserName AND Password=@Password ";
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.AddWithValue("@UserName","Admin");
                    sqlCommand.Parameters.AddWithValue("@Password", "Admin");
                    int count = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    if (count == 1)
                    {
                        Console.WriteLine("git");
                    }
                    else
                    {
                        MessageBox.Show("chuj");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
