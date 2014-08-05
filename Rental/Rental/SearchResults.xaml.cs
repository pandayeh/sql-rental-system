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
    /// Interaction logic for SearchResults.xaml
    /// </summary>
    public partial class SearchResults : Window
    {
        private SQLiteHelper helper;
        private DataTable table;
        private int id;

        public SearchResults(DataTable dt)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();

            helper = new SQLiteHelper();
            table = dt;
            gridSearch.DataContext = table.DefaultView;
        }

        public int getId { get { return id; } }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            int ind = gridSearch.SelectedIndex;
            id = Convert.ToInt32(table.Rows[ind].ItemArray[0].ToString());

            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            id = -1;
            Close();
        }
    }
}
