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
    /// Interaction logic for ManageCustomers.xaml
    /// </summary>
    public partial class ManageCustomers : Window
    {
        private DataTable table;
        private SQLiteHelper helper;

        public ManageCustomers()
        {
            InitializeComponent();

            helper = new SQLiteHelper();
            table = helper.GetDataTable(
                "SELECT customers.*, memberships.name AS memName " +
                "FROM customers, memberships " +
                "WHERE memberships.membershipId=customers.membershipId");
            gridCustomers.DataContext = table.DefaultView;
        }

        private void search_Enter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                table = helper.GetDataTable( //TODO: select from datatable instead of database?
                    "SELECT * " +
                    "FROM customers " +
                    "WHERE customers.code LIKE \"%" + search.Text + "%\" OR customers.name LIKE \"%" + search.Text + "%\"");
                gridCustomers.DataContext = table.DefaultView;
            }
            
        }

        //Data

        private void NewCustomer_Click(object sender, RoutedEventArgs e)
        {
            NewCustomer win = new NewCustomer();
            win.ShowDialog();
            table = helper.GetDataTable(
                    "SELECT * " +
                    "FROM customers " +
                    "WHERE customers.code LIKE \"%" + search.Text + "%\" OR customers.name LIKE \"%" + search.Text + "%\"");
            gridCustomers.DataContext = table.DefaultView;
        }

        private void EditCustomer_Click(object sender, RoutedEventArgs e)
        {
            EditCustomer win = new EditCustomer(Convert.ToInt32(gridCustomers.SelectedValue));
            win.ShowDialog();
            table = helper.GetDataTable(
                    "SELECT * " +
                    "FROM customers " +
                    "WHERE customers.code LIKE \"%" + search.Text + "%\" OR customers.name LIKE \"%" + search.Text + "%\"");
            gridCustomers.DataContext = table.DefaultView;
        }

        private void DeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            DataTable rented = helper.GetDataTable("SELECT rented.* FROM rented, customers WHERE rented.customerId=" + Convert.ToInt32(gridCustomers.SelectedValue));
            if(rented.Rows.Count != 0)
                MessageBox.Show("Error: Customer has active rentals.");
            else
            {
               if(MessageBox.Show(
                   "Are you sure you want to delete customer \"" + ((DataRowView)gridCustomers.SelectedItems[0])["name"] + "\"?", 
                   "Confirm Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
               {
                   helper.ExecuteNonQuery("DELETE FROM customers WHERE customerId=" + gridCustomers.SelectedValue);
                   table = helper.GetDataTable(
                        "SELECT customers.*, memberships.name AS memName " +
                        "FROM customers, memberships " +
                        "WHERE memberships.membershipId=customers.membershipId");
                   gridCustomers.DataContext = table.DefaultView;
               }
                   
            }
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //Logs

        private void Credit_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Debit_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void DepositCash_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void DepositOther_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
