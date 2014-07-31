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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Rental
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static SQLiteHelper helper = new SQLiteHelper();
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                this.clock.Text = DateTime.Now.ToString();
            }, this.Dispatcher);

            
            companyname.DataContext = helper.ExecuteScalar("SELECT name FROM companyinfo");
            

        }

        private void ManageInventory_Click(object sender, RoutedEventArgs e)
        {
            Inventory win = new Inventory();
            win.ShowDialog();
        }

        private void CompanyInfo_Click(object sender, RoutedEventArgs e)
        {
            CompanyInfoSetup win = new CompanyInfoSetup();
            win.ShowDialog();
            companyname.DataContext = helper.ExecuteScalar("SELECT name FROM companyinfo");
        }
    }
}
