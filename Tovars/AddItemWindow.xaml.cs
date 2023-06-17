using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
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
        public partial class AddItemWindow : Window
        {
            long? Idprodazhi;
            public AddItemWindow()
            {
                InitializeComponent();
          //  Idprodazhi = idprodazhi;
            }

            private void Window_Loaded(object sender, RoutedEventArgs e)
            {
                Listtovarov();
               // ListPokypatel();
                dateTxt.Text = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");
            }

            private void addBtn_Click(object sender, RoutedEventArgs e)
            {
                SqlConnection sqlConnection = new SqlConnection(@"Data Source=LENOVKA\SQLEXPRESS;Initial Catalog=Dipp;Integrated Security=True");

                try
                {

                    if (sqlConnection.State == ConnectionState.Closed)
                        sqlConnection.Open();

                    String query1 = $"set language english INSERT INTO Prodazha VALUES({tovarBox.SelectedValue}, {KolichTxt.Text},'{dateTxt.Text = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss")}', {ItogTxt.Text})";
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

            private void Listtovarov()
            {
                SqlConnection sqlConn = new SqlConnection(@"Data Source=LENOVKA\SQLEXPRESS;Initial Catalog=Dipp;Integrated Security=True");

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConn;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = "SELECT * from Tovar ";
                SqlDataAdapter sqlDa = new SqlDataAdapter();
                sqlDa.SelectCommand = sqlCommand;
                DataSet ds = new DataSet();
                try
                {

                    if (sqlConn.State == ConnectionState.Closed)
                        sqlConn.Open();


                    sqlDa.Fill(ds, "Tovar");

                //Binding the data to the combobox.
                tovarBox.DataContext = ds.Tables["Tovar"].DefaultView;

                //To display category name (DisplayMember in Visual Studio 2005)
                tovarBox.DisplayMemberPath =
                        ds.Tables["Tovar"].Columns["Nazvanie_tovara"].ToString();
                //To store the ID as hidden (ValueMember in Visual Studio 2005)
                tovarBox.SelectedValuePath =
                        ds.Tables["Tovar"].Columns["Id_tovara"].ToString();
                    sqlConn.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Возникла ошибка при попытке загрузить список товаров.\n" + ex.Message);
                }
            }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
           Close();
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
            // select Tovar.Cena*Prodazha.Kolichestvo_tovarov from Prodazha join Tovar on Tovar.Id_tovara=Prodazha.Id_tovaraa  where Id_prodazhi=1
        }

        private void tovarBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            KolichTxt.IsEnabled = true;


        }



        //private void ListPokypatel()
        //{
        //    SqlConnection sqlConnect = new SqlConnection(@"Data Source=LENOVKA\SQLEXPRESS;Initial Catalog=Dipp;Integrated Security=True");

        //    SqlCommand sqlComm = new SqlCommand();
        //    sqlComm.Connection = sqlConnect;
        //    sqlComm.CommandType = CommandType.Text;
        //    sqlComm.CommandText = "SELECT * FROM Pokypatel";
        //    SqlDataAdapter sqlDa = new SqlDataAdapter();
        //    sqlDa.SelectCommand = sqlComm;
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        if (sqlConnect.State == ConnectionState.Closed)
        //            sqlConnect.Open();

        //        sqlDa.Fill(ds, "Pokypatel");

        //    //Binding the data to the combobox.
        //    identityBox.DataContext = ds.Tables["Pokypatel"].DefaultView;

        //    //To display category name (DisplayMember in Visual Studio 2005)
        //    identityBox.DisplayMemberPath =
        //            ds.Tables["Pokypatel"].Columns["Identity_number"].ToString();
        //    //To store the ID as hidden (ValueMember in Visual Studio 2005)
        //    identityBox.SelectedValuePath =
        //            ds.Tables["Pokypatel"].Columns["Id_pokypatelya"].ToString();
        //        sqlConnect.Close();

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Возникла ошибка при попытке загрузить список покупателей.\n" + ex.Message);
        //    }
        //}

        //private void problemBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //identityBox.IsEnabled = true;
        //ListPokypatel();
        //}
    }
}

