using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для Report2.xaml
    /// </summary>
    public partial class Report2 : Window
    {
        public Report2()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Создание объекта ReportViewer
            this.reportViewer.ReportPath = System.IO.Path.Combine(Environment.CurrentDirectory, @"D:\kbip\Диплом\\BestClient.rdl");
            this.reportViewer.RefreshReport();

        }
    }

}

