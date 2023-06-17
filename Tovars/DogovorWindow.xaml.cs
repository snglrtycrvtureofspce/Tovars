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
using System.Windows.Forms;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;

namespace Diplom
{
    /// <summary>
    /// Логика взаимодействия для DogovorWindow.xaml
    /// </summary>
    public partial class DogovorWindow : Window
    {
        public DogovorWindow()
        {
            InitializeComponent();
        }

        SqlConnection sqlConnection = new SqlConnection(@"Data Source=LENOVKA\SQLEXPRESS;Initial Catalog=Dipp;Integrated Security=True");

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (sqlConnection.State == ConnectionState.Closed)
                sqlConnection.Open();

            String query1 = "select Id_dogovora as 'ID', Nazvanie_tovara as 'Название товара', FIO as 'ФИО', Pokypatel.Identity_number as 'Идентификационный номер', Dogovor.Data_prodazhi as 'Дата продажи', Kolichestvo_tovarov as 'Кол-во товаров' , Itogovaya_stoimost as 'Итоговая стоимость' " +
                "from Dogovor join Pokypatel on Dogovor.Id_pokupatelyaa = Pokypatel.Id_pokypatelya join Prodazha on Dogovor.Id_prodazhii = Prodazha.Id_prodazhi join Tovar on Prodazha.Id_tovaraa = Tovar.Id_tovara ";
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
        private void AddPokypatel_Click(object sender, RoutedEventArgs e)
        {
            AddClientWindow addClientWindow = new AddClientWindow();
            addClientWindow.ShowDialog();
            Update();
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

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
            searchBox.Items.Add(new { Name = "ФИО", Value = "FIO" });
            searchBox.Items.Add(new { Name = "Идентификационный номер", Value = "Identity_number" });
            searchBox.Items.Add(new { Name = "Дата продажи", Value = "Dogovor.Data_prodazhi" });
        }


        private void Update()
        {

            if (sqlConnection.State == ConnectionState.Closed)
                sqlConnection.Open();

            String query1 = "select Id_dogovora as 'ID', Nazvanie_tovara as 'Название товара', FIO as 'ФИО', Pokypatel.Identity_number as 'Идентификационный номер', Dogovor.Data_prodazhi as 'Дата продажи', Kolichestvo_tovarov as 'Кол-во товаров' , Itogovaya_stoimost as 'Итоговая стоимость' " +
                "from Dogovor join Pokypatel on Dogovor.Id_pokupatelyaa = Pokypatel.Id_pokypatelya join Prodazha on Dogovor.Id_prodazhii = Prodazha.Id_prodazhi join Tovar on Prodazha.Id_tovaraa = Tovar.Id_tovara ";
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

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (sqlConnection.State == ConnectionState.Closed)
                sqlConnection.Open();
            //  ComboBoxItem comboBoxItem = (ComboBoxItem)searchBox.SelectedItem;
            String query1 = "select Id_dogovora as 'ID', Nazvanie_tovara as 'Название товара', FIO as 'ФИО', Pokypatel.Identity_number as 'Идентификационный номер', Dogovor.Data_prodazhi as 'Дата продажи', Kolichestvo_tovarov as 'Кол-во товаров' , Itogovaya_stoimost as 'Итоговая стоимость' " +
                "from Dogovor join Pokypatel on Dogovor.Id_pokupatelyaa = Pokypatel.Id_pokypatelya " +
                "join Prodazha on Dogovor.Id_prodazhii = Prodazha.Id_prodazhi join Tovar on Prodazha.Id_tovaraa = Tovar.Id_tovara "+
            $"where {searchBox.SelectedValue} LIKE '%{searchTxt.Text}%'";

            SqlCommand sqlCommand = new SqlCommand(query1, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            dataGrid1.ItemsSource = dataTable.DefaultView;
            sqlConnection.Close();
        }



        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Получаем выбранную строку
            DataRowView selectedRow = dataGrid1.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                try
                {
                    // Получаем необходимые данные из выбранной строки
                    string dogID = selectedRow.Row["ID"].ToString();
                    string clientName = selectedRow.Row["ФИО"].ToString();
                    string contractDate = selectedRow.Row["Дата продажи"].ToString();
                string tovar_name = selectedRow.Row["Название товара"].ToString();
                string cost = selectedRow.Row["Итоговая стоимость"].ToString();
                string count = selectedRow.Row["Кол-во товаров"].ToString();
                // и т.д. - получите остальные данные

                // Путь к шаблону документа Word
                string templatePath = "D:\\kbip\\Диплом\\Dogovor.docx";

                // Путь для сохранения нового документа
                string outputPath = "D:\\kbip\\Диплом\\Dogovor2.docx";

                // Создаем копию шаблона
                File.Copy(templatePath, outputPath, true);

                // Заменяем метки в документе на необходимые значения
                using (WordprocessingDocument doc = WordprocessingDocument.Open(outputPath, true))
                {
                    // Получаем все текстовые элементы в документе
                    IEnumerable<Text> textElements = doc.MainDocumentPart.Document.Descendants<Text>();

                    foreach (Text text in textElements)
                    {
                        // Заменяем метки в текстовых элементах на соответствующие значения
                        if (text.Text == "[ФИО]")
                            text.Text = clientName;
                        else if (text.Text == "[ID]")
                            text.Text = dogID;
                        else if (text.Text == "[Дата продажи]")
                            text.Text = contractDate;
                        else if (text.Text == "[Название товара]")
                            text.Text = tovar_name; 
                        else if (text.Text == "[Итоговая стоимость]")
                            text.Text = cost;
                         else if (text.Text == "[Кол-во товаров]")
                            text.Text = count;
                        // и т.д. - добавьте замены для остальных полей
                    }
                }

                // Открываем созданный договор
                System.Diagnostics.Process.Start(outputPath);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
