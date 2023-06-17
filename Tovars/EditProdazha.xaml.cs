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
using DocumentFormat.OpenXml.Office2010.Excel;

namespace Diplom
{
    /// <summary>
    /// Логика взаимодействия для EditPokypatelWindow.xaml
    /// </summary>
    public partial class EditProdazha : Window
    {

        DataRowView rowView;
        int id = 0;


        public EditProdazha(DataRowView rowView)
        {
            InitializeComponent();
            this.rowView = rowView;
            
            ListTovars();
            UpdateLayout();

        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            id = Convert.ToInt32(rowView["ID"].ToString());


            tovarBox.SelectedItem = rowView["Название товара"];
            tovarBox.Text = rowView["Название товара"].ToString();

            dateTxt.Text = rowView["Дата продажи"].ToString();
            KolichTxt.Text = rowView["Кол-во товаров"].ToString();
            ItogTxt.Text = rowView["Итоговая стоимость"].ToString();

        }


        public void ListTovars()
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=LENOVKA\SQLEXPRESS;Initial Catalog=Dipp;Integrated Security=True");

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT * FROM Tovar";
            SqlDataAdapter sqlDa = new SqlDataAdapter();
            sqlDa.SelectCommand = sqlCommand;
            DataSet ds = new DataSet();

            try
            {

                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                sqlDa.Fill(ds, "Tovar");


                //Binding the data to the combobox.
                tovarBox.DataContext = ds.Tables["Tovar"].DefaultView;

                //To display category name (DisplayMember in Visual Studio 2005)
                tovarBox.DisplayMemberPath =
                    ds.Tables["Tovar"].Columns["Nazvanie_tovara"].ToString();
                //To store the ID as hidden (ValueMember in Visual Studio 2005)
                tovarBox.SelectedValuePath =
                            ds.Tables["Tovar"].Columns["Id_tovara"].ToString();

                //var comboBoxItem = comboBox.Items.OfType<ComboBoxItem>().FirstOrDefault(x => x.Content.ToString() == Level.ID_Level.ToString());
                //int index = comboBox.SelectedIndex = comboBox.Items.IndexOf(comboBoxItem);


            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading tovars.\n" + ex.Message);
            }
            finally
            {
                sqlDa.Dispose();
                sqlCommand.Dispose();
                sqlConnection.Dispose();
            }

        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=LENOVKA\SQLEXPRESS;Initial Catalog=Dipp;Integrated Security=True");

            try
            {

                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                String query1 = $"UPDATE Prodazha SET Id_tovaraa = {tovarBox.SelectedValue}, Data_prodazhii = '{dateTxt.Text}', Kolichestvo_tovarov = '{KolichTxt.Text}', Itogovaya_stoimost= {ItogTxt.Text} " +
                     $"WHERE Id_prodazhi = {id}";
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

        private void KolichTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=LENOVKA\SQLEXPRESS;Initial Catalog=Dipp;Integrated Security=True");


            if (sqlConnection.State == ConnectionState.Closed)
                sqlConnection.Open();

            try
            {

                String query = $"select Tovar.Cena*{KolichTxt.Text} as 'Stoimost' from  Tovar where Id_tovara={tovarBox.SelectedValue} ";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.CommandType = CommandType.Text;


                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {

                    if (reader.Read())
                    {
                        int? Stoimost = reader["Stoimost"] as int?;

                        ItogTxt.Text = Stoimost.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка при попытке ввода количества товаров.\n" + ex.Message);
            }
        }

        private void tovarBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=LENOVKA\SQLEXPRESS;Initial Catalog=Dipp;Integrated Security=True");


            if (sqlConnection.State == ConnectionState.Closed)
                sqlConnection.Open();

            try
            {

                String query = $"select Tovar.Cena*{KolichTxt.Text} as 'Stoimost' from  Tovar where Id_tovara={tovarBox.SelectedValue} ";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.CommandType = CommandType.Text;


                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {

                    if (reader.Read())
                    {
                        int? Stoimost = reader["Stoimost"] as int?;

                        ItogTxt.Text = Stoimost.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка при попытке ввода количества товаров.\n" + ex.Message);
            }
        }
    }
}
