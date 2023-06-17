using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Windows.Shapes;

namespace Diplom
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=LENOVKA\SQLEXPRESS;Initial Catalog=Dipp;Integrated Security=True");
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();


                String query = "use Dipp SELECT Id_user FROM [Dipp].[dbo].[Authorization] WHERE Login = @Login AND Password = @Password";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@Login", txtLogin.Text);
                sqlCommand.Parameters.AddWithValue("@Password", txtPassword.Password);


                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {

                    if (reader.Read())
                    {
                        long? rowView = reader["Id_user"] as long?;

                        MainWindow main = new MainWindow();
                        main.Show();
                        this.Close();
                                  
                    }                         
                    else
                    {
                        MessageBox.Show("Логин или пароль неверны");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

