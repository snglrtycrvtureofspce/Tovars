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
using System.Windows.Shapes;

namespace Diplom
{
    /// <summary>
    /// Логика взаимодействия для EditTovarWindow.xaml
    /// </summary>
    public partial class EditTovarWindow : Window
    {


        DataRowView rowView;
        int id = 0;


        public EditTovarWindow(DataRowView rowView)
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

            tovarTxt.Text = rowView["Название товара"].ToString();
            edTxt.Text = rowView["Еденица измерения"].ToString();
            cenaTxt.Text = rowView["Цена за единицу товара"].ToString();

        }



        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=LENOVKA\SQLEXPRESS;Initial Catalog=Dipp;Integrated Security=True");

            try
            {

                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                String query1 = $"UPDATE Tovar SET Nazvanie_tovara = '{tovarTxt.Text}', Ediniza_izm = {edTxt.Text}, Cena= {cenaTxt.Text}" +
                    $"WHERE Id_tovara = {id}";
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
