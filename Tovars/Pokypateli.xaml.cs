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
    public partial class Pokypateli : Window
    {
        public Pokypateli()
        {
            InitializeComponent();
        }

        SqlConnection sqlConnection = new SqlConnection(@"Data Source=LENOVKA\SQLEXPRESS;Initial Catalog=Dipp;Integrated Security=True");

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            if (sqlConnection.State == ConnectionState.Closed)
                sqlConnection.Open();

            String query1 = "select Id_pokypatelya as 'ID', FIO as 'ФИО', Identity_number as 'Идентификационный номер' from Pokypatel";
            SqlCommand sqlCommand = new SqlCommand(query1, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            dataGrid1.ItemsSource = dataTable.DefaultView;
            sqlConnection.Close();
            Zopolnenie();

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

        private void TipyTovarov_Click(object sender, RoutedEventArgs e)
        {
            TovarsWindow main = new TovarsWindow();
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
            Update();

        }

        private void Update()
        {

            if (sqlConnection.State == ConnectionState.Closed)
                sqlConnection.Open();

            String query1 = "select Id_pokypatelya as 'ID', FIO as 'ФИО', Identity_number as 'Идентификационный номер' from Pokypatel";
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

                    EditPokypatelWindow editAdminWindow = new EditPokypatelWindow(rowViews);
                    editAdminWindow.ShowDialog();
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

            searchBox.Items.Add(new { Name = "ФИО", Value = "FIO" });
            searchBox.Items.Add(new { Name = "Идентификационный номер", Value = "Identity_number" });
        }


        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (sqlConnection.State == ConnectionState.Closed)
                sqlConnection.Open();
            //  ComboBoxItem comboBoxItem = (ComboBoxItem)searchBox.SelectedItem;
            String query1 = "select Id_pokypatelya as 'ID', FIO as 'ФИО', Identity_number as 'Идентификационный номер' from Pokypatel " +
                $"where {searchBox.SelectedValue} LIKE '%{searchTxt.Text}%'";

            SqlCommand sqlCommand = new SqlCommand(query1, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            dataGrid1.ItemsSource = dataTable.DefaultView;
            sqlConnection.Close();
        }



        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rowView = dataGrid1.SelectedValue as DataRowView;


            if (rowView != null)
            {
                try
                {
                    MessageBoxResult result = System.Windows.MessageBox.Show("Вы уверены что хотите удалить покупателя?", "Удаление покупателя", MessageBoxButton.YesNo);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            {
                                var indexDataTable = rowView[0];

                                var indexDataGridRow = dataGrid1.SelectedIndex;//тут индекс самой строки, чтобы удалить ее из DataTable, либо вообще DataTable заново получать

                                using (SqlCommand cmd = new SqlCommand())
                                {
                                    cmd.Connection = sqlConnection;
                                    cmd.CommandText = $"DELETE FROM Dogovor where Id_pokupatelyaa = '{indexDataTable}' " +
                                        $"DELETE FROM Pokypatel where Id_pokypatelya = '{indexDataTable}'";
                                    System.Windows.MessageBox.Show("Пользователь удален!");

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


        private void AddPokypatel_Click(object sender, RoutedEventArgs e)
        {
            AddClientWindow addClientWindow = new AddClientWindow();
            addClientWindow.ShowDialog();
            Update();
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
