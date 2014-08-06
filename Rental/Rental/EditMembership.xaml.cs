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
    /// Interaction logic for EditMembership.xaml
    /// </summary>
    public partial class EditMembership : Window
    {
        private SQLiteHelper helper;
        private DataTable table;
        private int membershipId;

        public EditMembership(int mid)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();

            membershipId = mid;
            helper = new SQLiteHelper();

            DataTable oldvals = helper.GetDataTable("SELECT name, deposit, discount FROM memberships WHERE membershipId=" + membershipId);
            
            if ((double)table.Rows[0].ItemArray[2] == 0) //If no discount
                noDiscount.IsChecked = true;
            else discountButton.IsChecked = true;

            name.Text = table.Rows[0].ItemArray[0].ToString();
            deposit.Text = table.Rows[0].ItemArray[1].ToString();
            discountPercent.Text = table.Rows[0].ItemArray[2].ToString();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (deposit.Text.Contains("^[0-9]") || discountPercent.Text.Contains("^[0-9]"))
                MessageBox.Show("Deposit and Discount must be numbers.");
            else
                helper.ExecuteNonQuery("UPDATE memberships " +
                    "SET " + 
                    "name=\"" + name.Text + "\", " + 
                    "deposit=\"" + deposit.Text + "\", " +
                    "discount=\"" + ((bool)(discountButton.IsChecked) ? discountPercent.Text : "0") + "\" " +//Discount or not
                    "WHERE membershipId="+membershipId);

            Close();

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
