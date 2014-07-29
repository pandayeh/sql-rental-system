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
        public Serials()
        {
            InitializeComponent();
            SQLiteHelper helper = new SQLiteHelper();
            DataTable invTable = helper.GetDataTable( //TODO: Implement volume sets
                "SELECT types.name AS Type, series.title AS Title, serials.volume AS Volume, series.ongoing AS 疵, series.artist AS Author, series.publisher AS Publisher, serials.serial AS Serial, series.reference AS Reference, serials.price AS Price " +
                "FROM serials, series, types " +
                "WHERE series.seriesId=serials.titleId AND types.typeId=series.typeId");
            gridSerial.DataContext = invTable.DefaultView;
        }
    }
}
