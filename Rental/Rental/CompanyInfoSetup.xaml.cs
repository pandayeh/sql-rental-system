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

namespace Rental
{
    /// <summary>
    /// Interaction logic for CompanyInfoSetup.xaml
    /// </summary>
    public partial class CompanyInfoSetup : Window
    {
        public CompanyInfoSetup()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            SQLiteHelper helper = new SQLiteHelper();
            string sql = "UPDATE companyinfo " +
                "SET name=\"" + name.Text + "\", rentDays=";

            if ((bool)days1.IsChecked)
                sql += "0";
            else sql += rentDays.Text;
            sql += ", rentQuantity=";

            if ((bool)quantity1.IsChecked)
                sql += "0";
            else sql += rentQuantity.Text;
            sql += " WHERE rowid=1;";
            helper.ExecuteNonQuery(sql);

            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
