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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WebScrapingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Scrapper scrapper;
        public MainWindow()
        {
            InitializeComponent();
            scrapper = new Scrapper();
            DataContext = scrapper;
        }

        private void BtnScrapper_Click(object sender, RoutedEventArgs e)
        {
            scrapper.ScrapeData(TbPage.Text);
        }
        private void ItemExport_Click(object sender, RoutedEventArgs e)
        {
            scrapper.Export();
        }
    }
}
