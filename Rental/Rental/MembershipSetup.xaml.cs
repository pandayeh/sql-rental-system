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
    /// Interaction logic for MembershipSetup.xaml
    /// </summary>
    public partial class MembershipSetup : Window
    {
        public MembershipSetup()
        {
            InitializeComponent();
            SQLiteHelper helper = new SQLiteHelper();
            DataTable table = helper.GetDataTable("SELECT types.name AS Type FROM types");
            gridMemberships.DataContext = table.DefaultView;
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Finish_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
