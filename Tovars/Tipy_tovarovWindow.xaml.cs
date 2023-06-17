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
    /// Логика взаимодействия для DogovorWindow.xaml
    /// </summary>
    public partial class Tipy_tovarovWindow : Window
    {
        public Tipy_tovarovWindow()
        {
            InitializeComponent();
        }

        SqlConnection sqlConnection = new SqlConnection(@"Data Source=LENOVKA\SQLEXPRESS;Initial Catalog=Dipp;Integrated Security=True");

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            if (sqlConnection.State == ConnectionState.Closed)
                sqlConnection.Open();

            String query1 = "select Id_tipa as 'ID', Nazvanie as 'Название типа товара', Harakteristiki as 'Характеристики товара' from Tipy_tovarov";
            SqlCommand sqlCommand = new SqlCommand(query1, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            dataGrid1.ItemsSource = dataTable.DefaultView;
            sqlConnection.Close();
            Zopolnenie();

        }



        private void Zopolnenie()
        {
            searchBox.DisplayMemberPath = "Name";
            searchBox.SelectedValuePath = "Value";

            searchBox.Items.Add(new { Name = "Название типа товара", Value = "Nazvanie" });
            searchBox.Items.Add(new { Name = "Характеристики", Value = "Harakteristiki" });
        }


        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (sqlConnection.State == ConnectionState.Closed)
                sqlConnection.Open();
            //  ComboBoxItem comboBoxItem = (ComboBoxItem)searchBox.SelectedItem;
            String query1 = "select Id_tipa as 'ID', Nazvanie as 'Название типа товара', Harakteristiki as 'Характеристики товара' from Tipy_tovarov " +
                $"where {searchBox.SelectedValue} LIKE '%{searchTxt.Text}%'";

            SqlCommand sqlCommand = new SqlCommand(query1, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            dataGrid1.ItemsSource = dataTable.DefaultView;
            sqlConnection.Close();
        }


        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddItemWindow addItemWindow = new AddItemWindow();
            addItemWindow.ShowDialog();

        }

        private void Dogovory_Click(object sender, RoutedEventArgs e)
        {
            DogovorWindow main = new DogovorWindow();
            main.Show();
            this.Close();
        }

        private void Pokupately_Click(object sender, RoutedEventArgs e)
        {
            Pokypateli main = new Pokypateli();
            main.Show();
            this.Close();
        }

        private void Prodazhi_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (sqlConnection.State == ConnectionState.Closed)
                sqlConnection.Open();

            String query1 = "select Id_tipa as 'ID', Nazvanie as 'Название типа товара', Harakteristiki as 'Характеристики товара' from Tipy_tovarov"; ;
            SqlCommand sqlCommand = new SqlCommand(query1, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            dataGrid1.ItemsSource = dataTable.DefaultView;
            sqlConnection.Close();
        }
        private void ShowHelp()
        {
            System.Diagnostics.Process.Start("D:\\Visual studddio\\Diplom\\Diplom\\Spravka.chm");
        }
        private void MenuItem_Clickk(object sender, RoutedEventArgs e)
        {
            ShowHelp();
        }
        private void AddPokypatel_Click(object sender, RoutedEventArgs e)
        {
            AddClientWindow addClientWindow = new AddClientWindow();
            addClientWindow.Show();
            this.Close();
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
