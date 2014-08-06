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
    public partial class NewMembership : Window
    {
        private SQLiteHelper helper;

        public NewMembership()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();

            helper = new SQLiteHelper();

        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (deposit.Text.Contains("^[0-9]") || discountPercent.Text.Contains("^[0-9]"))
                MessageBox.Show("Deposit and Discount must be numbers.");
            else
                helper.ExecuteNonQuery("INSERT INTO memberships (name,deposit,discount) " +
                    "VALUES (\"" + name.Text + "\"," + deposit.Text + "," + 
                ((bool)(discountButton.IsChecked) ? discountPercent.Text : "0") + ")");

            Close();

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
