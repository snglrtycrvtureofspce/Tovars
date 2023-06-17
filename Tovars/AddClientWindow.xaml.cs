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
    public partial class AddClientWindow : Window
    {
        long? Idprodazhi;
        public AddClientWindow()
        {
            InitializeComponent();
            //  Idprodazhi = idprodazhi;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           // Listtovarov();
            // ListPokypatel();
          //  dateTxt.Text = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=LENOVKA\SQLEXPRESS;Initial Catalog=Dipp;Integrated Security=True");

            try
            {

                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                String query1 = $"use Dipp INSERT INTO Pokypatel VALUES ('{FIOTxt.Text}', '{IdentityTxt.Text}')";
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

        //private void Listtovarov()
        //{
        //    SqlConnection sqlConn = new SqlConnection(@"Data Source=LENOVKA\SQLEXPRESS;Initial Catalog=Dipp;Integrated Security=True");

        //    SqlCommand sqlCommand = new SqlCommand();
        //    sqlCommand.Connection = sqlConn;
        //    sqlCommand.CommandType = CommandType.Text;
        //    sqlCommand.CommandText = "SELECT * from Pokypatel ";
        //    SqlDataAdapter sqlDa = new SqlDataAdapter();
        //    sqlDa.SelectCommand = sqlCommand;
        //    DataSet ds = new DataSet();
        //    try
        //    {

        //        if (sqlConn.State == ConnectionState.Closed)
        //            sqlConn.Open();


        //        sqlDa.Fill(ds, "Pokypatel");

        //        //Binding the data to the combobox.
        //        FmilyaTxt.DataContext = ds.Tables["Pokypatel"].DefaultView;

        //        //To display category name (DisplayMember in Visual Studio 2005)
        //        FmilyaTxt.DisplayMemberPath =
        //                ds.Tables["Tovar"].Columns["Nazvanie_tovara"].ToString();
        //        //To store the ID as hidden (ValueMember in Visual Studio 2005)
        //        FmilyaTxt.SelectedValuePath =
        //                ds.Tables["Tovar"].Columns["Id_tovara"].ToString();
        //        sqlConn.Close();

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Возникла ошибка при попытке загрузить список товаров.\n" + ex.Message);
        //    }
        //}

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
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

