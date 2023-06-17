using DocumentFormat.OpenXml.Wordprocessing;
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
    /// Логика взаимодействия для AddTovarWindow1.xaml
    /// </summary>
    public partial class AddTovarWindow1 : Window
    {
        public AddTovarWindow1()
        {
            InitializeComponent();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=LENOVKA\SQLEXPRESS;Initial Catalog=Dipp;Integrated Security=True");

            try
            {

                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                String query1 = $"use Dipp INSERT INTO Tovar VALUES ('{tovarTxt.Text}', '{edTxt.Text}', {edTxt_Copy.Text} )";
                //{identityBox.SelectedValue}" 

                SqlCommand sqlCommand = new SqlCommand(query1, sqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
