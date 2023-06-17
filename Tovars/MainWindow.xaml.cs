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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        SqlConnection sqlConnection = new SqlConnection(@"Data Source=LENOVKA\SQLEXPRESS;Initial Catalog=Dipp;Integrated Security=True");

        public MainWindow()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            if (sqlConnection.State == ConnectionState.Closed)
                sqlConnection.Open();

            String query1 = "select Id_prodazhi as 'ID', Nazvanie_tovara as 'Название товара', Kolichestvo_tovarov as 'Кол-во товаров', Data_prodazhii as 'Дата продажи', Itogovaya_stoimost as 'Итоговая стоимость' " +
                "from Prodazha join Tovar on Prodazha.Id_tovaraa = Tovar.Id_tovara";
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
            Update();
        }

        private void Pokupately_Click(object sender, RoutedEventArgs e)
        {
            Pokypateli main = new Pokypateli();
            main.Show();
            this.Close();
        }

        private void TipyTovarov_Click(object sender, RoutedEventArgs e)
        {
            TovarsWindow main = new TovarsWindow();
            main.Show();
            this.Close();
        }

        private void Dogovory_Click(object sender, RoutedEventArgs e)
        {
            DogovorWindow dogovorWindow = new DogovorWindow(); 
            dogovorWindow.Show();
            this.Close();
        }


        public void Update()
        {
            if (sqlConnection.State == ConnectionState.Closed)
                sqlConnection.Open();

            String query1 = "select Id_prodazhi as 'ID', Nazvanie_tovara as 'Название товара', Kolichestvo_tovarov as 'Кол-во товаров', Data_prodazhii as 'Дата продажи', Itogovaya_stoimost as 'Итоговая стоимость' " +
                "from Prodazha join Tovar on Prodazha.Id_tovaraa = Tovar.Id_tovara";
            SqlCommand sqlCommand = new SqlCommand(query1, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            DataTable dataTable = new DataTable();

            dataTable.Load(reader);
            dataGrid1.ItemsSource = dataTable.DefaultView;
            sqlConnection.Close();
        }


        private void Update_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void Zopolnenie()
        {
            searchBox.DisplayMemberPath = "Name";
            searchBox.SelectedValuePath = "Value";

            searchBox.Items.Add(new { Name = "Название товара", Value = "Nazvanie_tovara" });
            searchBox.Items.Add(new { Name = "Кол-во товаров", Value = "Kolichestvo_tovarov" });
            searchBox.Items.Add(new { Name = "Дата продажи", Value = "Data_prodazhii" });
            searchBox.Items.Add(new { Name = "Итоговая стоимость", Value = "Itogovaya_stoimost" });
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (sqlConnection.State == ConnectionState.Closed)
                sqlConnection.Open();
            //  ComboBoxItem comboBoxItem = (ComboBoxItem)searchBox.SelectedItem;
            String query1 = "select Id_prodazhi as 'ID', Nazvanie_tovara as 'Название товара', Kolichestvo_tovarov as 'Кол-во товаров', Data_prodazhii as 'Дата продажи', Itogovaya_stoimost as 'Итоговая стоимость' " +
                "from Prodazha join Tovar on Prodazha.Id_tovaraa = Tovar.Id_tovara " +
                $"where {searchBox.SelectedValue} LIKE '%{searchTxt.Text}%'";

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

                    EditProdazha EditProdazha = new EditProdazha(rowViews);
                    EditProdazha.ShowDialog();
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
                    MessageBoxResult result = System.Windows.MessageBox.Show("Вы уверены что хотите удалить продажу?", "Удаление продажи", MessageBoxButton.YesNo);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            {
                                var indexDataTable = rowView[0];

                                var indexDataGridRow = dataGrid1.SelectedIndex;//тут индекс самой строки, чтобы удалить ее из DataTable, либо вообще DataTable заново получать

                                using (SqlCommand cmd = new SqlCommand())
                                {
                                    cmd.Connection = sqlConnection;
                                    cmd.CommandText = $"DELETE FROM Dogovor where Id_prodazhii = '{indexDataTable}' " +
                                        $"DELETE FROM Prodazha where Id_prodazhi = '{indexDataTable}'";
                                    System.Windows.MessageBox.Show("Продажа удалена!");

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

        private void AddDogovor_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rowViews = dataGrid1.SelectedValue as DataRowView;

            try
            {
                if (rowViews != null)
                {

                    AddDogovorWindow1 addDogovorWindow1 = new AddDogovorWindow1(rowViews);
                    addDogovorWindow1.ShowDialog();

                }
                else
                    System.Windows.MessageBox.Show("Выделите строку для редактирования!");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
            try
            {
                DataRowView dataRowView = (DataRowView)((Button)sender).DataContext;
                String Idprodazhi = dataRowView[1].ToString();
                String Dataprodazhi = dataRowView[2].ToString();
               
                MessageBox.Show("Id продажи: " + Idprodazhi + "\nДата продажи: " + Dataprodazhi );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }
       

        private void AddPokypatel_Click(object sender, RoutedEventArgs e)
        {
            AddClientWindow addClientWindow = new AddClientWindow();
            addClientWindow.ShowDialog();
        }

      

        private void Prodazha_otchet_Click(object sender, RoutedEventArgs e)
        {
            //Otchet otchet = new Otchet();
            //otchet.Show();
            //this.Close();
        }

        private void Create_Dogovor(object sender, RoutedEventArgs e)
        {
            DataRowView rowViews = dataGrid1.SelectedValue as DataRowView;
           //DataGridRow rowContext = e.Row;
            //DataRowView dataRowView = DataContext as DataRowView;

            try
            {
                if (rowViews != null)
                {
                    AddDogovorWindow1 AddDogovorWindow1 = new AddDogovorWindow1(rowViews);
                    AddDogovorWindow1.ShowDialog();
                    Update();

                }
                else
                    System.Windows.MessageBox.Show("Выделите строку для создания договора!");
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
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ShowHelp();
        }

        private void BestTovar_Click(object sender, RoutedEventArgs e)
        {
            Report1 report1 = new Report1();
            report1.ShowDialog();
        }

        private void BestClient_Click(object sender, RoutedEventArgs e)
        {
            Report2 report2 = new Report2();
            report2.ShowDialog();
        }
        
        private void Kolvo_Click(object sender, RoutedEventArgs e)
        {
            Report3 report2 = new Report3();
            report2.ShowDialog();
        }

    }
}
