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
        private SQLiteHelper helper;
        private DataTable table;

        public MembershipSetup()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            
            helper = new SQLiteHelper();
            table = helper.GetDataTable("SELECT * FROM memberships");
            gridMemberships.DataContext = table.DefaultView;
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            NewMembership win = new NewMembership();
            win.ShowDialog();
            table = helper.GetDataTable("SELECT * FROM memberships");
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
