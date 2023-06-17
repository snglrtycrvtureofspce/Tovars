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
    /// Логика взаимодействия для TovarsWindow.xaml
    /// </summary>
    public partial class TovarsWindow : Window
    {
        public TovarsWindow()
        {
            InitializeComponent();
        }

        SqlConnection sqlConnection = new SqlConnection(@"Data Source=LENOVKA\SQLEXPRESS;Initial Catalog=Dipp;Integrated Security=True");

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            if (sqlConnection.State == ConnectionState.Closed)
                sqlConnection.Open();

            String query1 = "select Id_tovara as 'ID', Nazvanie_tovara as 'Название товара', Ediniza_izm as 'Еденица измерения', Cena as 'Цена за единицу товара' from Tovar";
            SqlCommand sqlCommand = new SqlCommand(query1, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            dataGrid1.ItemsSource = dataTable.DefaultView;
            sqlConnection.Close();
            Zopolnenie();

        }

        private void ShowHelp()
        {
            System.Diagnostics.Process.Start("D:\\Visual studddio\\Diplom\\Diplom\\Spravka.chm");
        }
        private void MenuItem_Clickk(object sender, RoutedEventArgs e)
        {
            ShowHelp();
        }

        private void Zopolnenie()
        {
            searchBox.DisplayMemberPath = "Name";
            searchBox.SelectedValuePath = "Value";

            searchBox.Items.Add(new { Name = "Название товара", Value = "Nazvanie_tovara" });
            searchBox.Items.Add(new { Name = "Цена за единицу товара", Value = "Cena" });
        }


        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (sqlConnection.State == ConnectionState.Closed)
                sqlConnection.Open();
            //  ComboBoxItem comboBoxItem = (ComboBoxItem)searchBox.SelectedItem;
            String query1 = "select Id_tovara as 'ID', Nazvanie_tovara as 'Название товара', Ediniza_izm as 'Еденица измерения', Cena as 'Цена за единицу товара' from Tovar " +
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
        private void Update()
        {
            if (sqlConnection.State == ConnectionState.Closed)
                sqlConnection.Open();

            String query1 = "select Id_tovara as 'ID', Nazvanie_tovara as 'Название товара', Ediniza_izm as 'Еденица измерения', Cena as 'Цена за единицу товара' from Tovar"; ;
            SqlCommand sqlCommand = new SqlCommand(query1, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            dataGrid1.ItemsSource = dataTable.DefaultView;
            sqlConnection.Close();
        }



        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rowViews = dataGrid1.SelectedValue as DataRowView;

            try
            {
                if (rowViews != null)
                {
                    EditTovarWindow EditTovarWindow = new EditTovarWindow(rowViews);
                    EditTovarWindow.ShowDialog();
                    Update();

                }
                else
                    System.Windows.MessageBox.Show("Выделите строку для редактирования!");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }



        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rowView = dataGrid1.SelectedValue as DataRowView;


            if (rowView != null)
            {
                try
                {
                    MessageBoxResult result = System.Windows.MessageBox.Show("Вы уверены что хотите удалить товар?", "Удаление товара", MessageBoxButton.YesNo);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            {
                                var indexDataTable = rowView[0];

                                var indexDataGridRow = dataGrid1.SelectedIndex;//тут индекс самой строки, чтобы удалить ее из DataTable, либо вообще DataTable заново получать

                                using (SqlCommand cmd = new SqlCommand())
                                {
                                    cmd.Connection = sqlConnection;
                                   // cmd.CommandText = $"DELETE FROM Dogovor where Id_prodazhii = (select Id_prodazhi from Prodazha where Id_tovaraa= {indexDataTable}) " +
                                    cmd.CommandText = $"DELETE FROM Dogovor WHERE Id_prodazhii IN (SELECT Id_prodazhi FROM Prodazha WHERE Id_tovaraa = {indexDataTable}) " +
                                        $"DELETE FROM Prodazha WHERE Id_tovaraa = '{indexDataTable}' "+
                                        $"DELETE FROM Tovar where Id_tovara = '{indexDataTable}'";
                                    System.Windows.MessageBox.Show("Товар удален!");

                                    cmd.Connection.Open();
                                    cmd.ExecuteNonQuery();
                                    cmd.Connection.Close();
                                }
                                Update();
                                break;
                            }

                        case MessageBoxResult.No:
                            {
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            }

            else
                System.Windows.MessageBox.Show("Выделите строку для удаления!");
        }


        private void Update_Click(object sender, RoutedEventArgs e)
        {
            Update();

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

        private void AddTovar_Click(object sender, RoutedEventArgs e)
        {
            AddTovarWindow1 add = new AddTovarWindow1();
            add.ShowDialog();
            Update();

        }
    }
}
