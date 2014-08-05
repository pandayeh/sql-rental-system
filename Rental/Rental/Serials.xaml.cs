using System;
using System.Collections.Generic;
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

namespace Rental
{
    /// <summary>
    /// Interaction logic for Serials.xaml
    /// </summary>
    public partial class Serials : Window
    {
        private int seriesId;
        public Serials(int i)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            seriesId = i;
            SQLiteHelper helper = new SQLiteHelper();
            DataTable table = helper.GetDataTable(
                "SELECT serials.setIndex AS Set, serials.volume AS Volume, serials.serial AS Serial, serials.price AS Price, serials.hotPrice AS Hot Price, serials.hot AS Hot " +
                "FROM serials, series " +
                "WHERE " + seriesId + "=serials.seriesId");
            gridSerial.DataContext = table.DefaultView;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Finish_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
