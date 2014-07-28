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
using System.Windows.Threading;

namespace Rental
{
    /// <summary>
    /// Interaction logic for Inventory.xaml
    /// </summary>
    public partial class Inventory : Window
    {
        public Inventory()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                this.clock.Text = DateTime.Now.ToString();
            }, this.Dispatcher);

            SQLiteHelper helper = new SQLiteHelper();
            DataTable invTable = helper.GetDataTable( //TODO: Implement volume sets
                "SELECT types.name AS Type, series.title AS Title, serials.volume AS Volume, series.ongoing AS 疵, series.artist AS Author, series.publisher AS Publisher, serials.serial AS Serial, series.reference AS Reference, serials.price AS Price " + 
                "FROM serials, series, types " + 
                "WHERE series.seriesId=serials.titleId AND types.typeId=series.typeId");
            gridInv.DataContext = invTable.DefaultView;

        }

        private void NewItem_Click(object sender, RoutedEventArgs e)
        {
            NewItem win = new NewItem();
            win.Show();
        }
    }
}
