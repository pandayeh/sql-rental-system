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

            deposit.DataContext = "Deposit: ";
            discount.DataContext = "Discount: ";
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            NewMembership win = new NewMembership();
            win.ShowDialog();
            table = helper.GetDataTable("SELECT * FROM memberships");
            gridMemberships.DataContext = table.DefaultView;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EditMembership win = new EditMembership(Convert.ToInt32(gridMemberships.SelectedValue));
                win.ShowDialog();
                table = helper.GetDataTable("SELECT * FROM memberships");
                gridMemberships.DataContext = table.DefaultView;
            }
            catch (Exception oops) { }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DataTable children = helper.GetDataTable("SELECT * FROM customers WHERE membershipId=" + Convert.ToInt32(gridMemberships.SelectedValue));
            if(children.Rows.Count != 0)
                MessageBox.Show("Error: There exist customers with this membership.");
            else
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this membership type?","Confirm", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    helper.ExecuteNonQuery("DELETE FROM memberships WHERE membershipId=" + gridMemberships.SelectedValue);
                    table = helper.GetDataTable("SELECT * FROM memberships");
                    gridMemberships.DataContext = table.DefaultView;
                }
            }
        }

        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Select_Type(object sender, RoutedEventArgs e)
        {
            try
            {
                deposit.DataContext = "Deposit: " + table.Rows[gridMemberships.SelectedIndex].ItemArray[2];
                discount.DataContext = "Discount: " + table.Rows[gridMemberships.SelectedIndex].ItemArray[3];
            }
            catch(Exception oops) {} //New Membership selects new row before table can update, causing this exception
        }

    }
}
