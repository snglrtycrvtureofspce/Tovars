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
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace Diplom
{
    /// <summary>
    /// Логика взаимодействия для AddDogovorWindow1.xaml
    /// </summary>
    public partial class AddDogovorWindow1 : Window
    {
        DataRowView rowView;

        int id;
        public AddDogovorWindow1(DataRowView rowView)
        {
            InitializeComponent();

            this.rowView = rowView;

            ListPokypatel();
            ListTovars();

            UpdateLayout();

        }




        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            SqlConnection sqlConnection = new SqlConnection(@"Data Source=LENOVKA\SQLEXPRESS;Initial Catalog=Dipp;Integrated Security=True");

            id = Convert.ToInt32(rowView["ID"].ToString());

            tovarBox.SelectedItem = rowView["Название товара"];
            tovarBox.Text = rowView["Название товара"].ToString();

            dateTxt.Text = rowView["Дата продажи"].ToString();
            kolvoTxt.Text = rowView["Кол-во товаров"].ToString();
            stoimostTxt.Text = rowView["Итоговая стоимость"].ToString();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=LENOVKA\SQLEXPRESS;Initial Catalog=Dipp;Integrated Security=True");

            try
            {

                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                String query1 = $"set language english INSERT INTO Dogovor VALUES({id}, {identityBox.SelectedValue}, '{dateTxt.Text = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss")}')";

                SqlCommand sqlCommand = new SqlCommand(query1, sqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ListPokypatel()
        {
            SqlConnection sqlConnect = new SqlConnection(@"Data Source=LENOVKA\SQLEXPRESS;Initial Catalog=Dipp;Integrated Security=True");

            SqlCommand sqlComm = new SqlCommand();
            sqlComm.Connection = sqlConnect;
            sqlComm.CommandType = CommandType.Text;
            sqlComm.CommandText = "SELECT * FROM Pokypatel";
            SqlDataAdapter sqlDa = new SqlDataAdapter();
            sqlDa.SelectCommand = sqlComm;
            DataSet ds = new DataSet();
            try
            {
                if (sqlConnect.State == ConnectionState.Closed)
                    sqlConnect.Open();

                sqlDa.Fill(ds, "Pokypatel");

                //Binding the data to the combobox.
                identityBox.DataContext = ds.Tables["Pokypatel"].DefaultView;

                //To display category name (DisplayMember in Visual Studio 2005)
                identityBox.DisplayMemberPath =
                        ds.Tables["Pokypatel"].Columns["FIO"].ToString();
                //To store the ID as hidden (ValueMember in Visual Studio 2005)
                identityBox.SelectedValuePath =
                        ds.Tables["Pokypatel"].Columns["Id_pokypatelya"].ToString();
                sqlConnect.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка при попытке загрузить список покупателей.\n" + ex.Message);
            }
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

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
