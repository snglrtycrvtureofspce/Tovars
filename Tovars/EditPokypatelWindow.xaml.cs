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
    /// Логика взаимодействия для EditPokypatelWindow.xaml
    /// </summary>
    public partial class EditPokypatelWindow : Window
    {

        DataRowView rowView;
        int id = 0;
       
            public EditPokypatelWindow(DataRowView rowView)
        {
            InitializeComponent();
            this.rowView = rowView;
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=LENOVKA\SQLEXPRESS;Initial Catalog=Dipp;Integrated Security=True");
            InitializeComponent();
            UpdateLayout();

        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            id = Convert.ToInt32(rowView["ID"].ToString());

            txtName_Copy.Text = rowView["ФИО"].ToString();
            txtPassword.Text = rowView["Идентификационный номер"].ToString();

        }



        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=LENOVKA\SQLEXPRESS;Initial Catalog=Dipp;Integrated Security=True");

            try
            {

                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                String query1 = $"UPDATE Pokypatel SET FIO = '{txtName_Copy.Text}', Identity_number = '{txtPassword.Text}'" +
                    $"WHERE Id_pokypatelya = {id}";
                SqlCommand sqlCommand = new SqlCommand(query1, sqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                MessageBox.Show("Успешно отредактировано!");

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
