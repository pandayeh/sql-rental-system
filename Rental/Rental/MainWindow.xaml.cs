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
        private SQLiteHelper helper;
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate //Clock
            {
                this.clock.Text = DateTime.Now.ToString();
            }, this.Dispatcher);

            helper = new SQLiteHelper();
            companyname.DataContext = helper.ExecuteScalar("SELECT name FROM companyinfo");
            

        }

        //Searchbar

        private void Search_Enter(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return)
                return;

            DataTable table = helper.GetDataTable( //TODO: Implement volume sets
                "SELECT customers.customerId, customers.code AS Code, customers.name AS Name " +
                "FROM customers " +
                "WHERE customers.code LIKE '%" + searchBox.Text + "%'");
            int count = table.Rows.Count;
            CustomerRent win;
            int id;
            switch (count)
            {
                case 0: MessageBox.Show("Customer not found."); break;

                case 1:
                    id = Convert.ToInt32(table.Rows[0].ItemArray[0].ToString());
                    win = new CustomerRent(id);
                    win.ShowDialog();
                    break;

                default:
                    SearchResults search = new SearchResults(table);
                    search.ShowDialog();
                    id = search.getId;
                    if (id != -1)
                    {
                        win = new CustomerRent(id);
                        win.ShowDialog();
                    }
                    break;
            }
        }

        //Business

        private void Rent_Click(object sender, RoutedEventArgs e)
        {
            EnterCustomerCodeRent win = new EnterCustomerCodeRent();
            win.ShowDialog();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Inventory win = new Inventory();
            win.ShowDialog();
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            Inventory win = new Inventory();
            win.ShowDialog();
        }

        private void InStore_Click(object sender, RoutedEventArgs e)
        {
            Inventory win = new Inventory();
            win.ShowDialog();
        }

        private void SwitchShifts_Click(object sender, RoutedEventArgs e)
        {
            Inventory win = new Inventory();
            win.ShowDialog();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Inventory win = new Inventory();
            win.ShowDialog();
        }

        //Data

        private void NewCustomer_Click(object sender, RoutedEventArgs e)
        {
            Inventory win = new Inventory();
            win.ShowDialog();
        }

        private void ManageCustomers_Click(object sender, RoutedEventArgs e)
        {
            Inventory win = new Inventory();
            win.ShowDialog();
        }

        private void ManageInventory_Click(object sender, RoutedEventArgs e)
        {
            Inventory win = new Inventory();
            win.ShowDialog();
        }

        private void NewItem_Click(object sender, RoutedEventArgs e)
        {
            Inventory win = new Inventory();
            win.ShowDialog();
        }

        private void RentedOutLog_Click(object sender, RoutedEventArgs e)
        {
            Inventory win = new Inventory();
            win.ShowDialog();
        }

        //Setup

        private void MembershipTypeSetup_Click(object sender, RoutedEventArgs e)
        {
            MembershipSetup win = new MembershipSetup();
            win.ShowDialog();
        }

        private void CreditPrepaySetup_Click(object sender, RoutedEventArgs e)
        {
            Inventory win = new Inventory();
            win.ShowDialog();
        }

        private void InventoryTypeSetup_Click(object sender, RoutedEventArgs e)
        {
            Inventory win = new Inventory();
            win.ShowDialog();
        }

        private void EmployeeSetup_Click(object sender, RoutedEventArgs e)
        {
            Inventory win = new Inventory();
            win.ShowDialog();
        }

        private void VendorSetup_Click(object sender, RoutedEventArgs e)
        {
            Inventory win = new Inventory();
            win.ShowDialog();
        }

        private void CustomerIDSetup_Click(object sender, RoutedEventArgs e)
        {
            Inventory win = new Inventory();
            win.ShowDialog();
        }

        //Management

        private void CashRegisterTotal_Click(object sender, RoutedEventArgs e)
        {
            CompanyInfoSetup win = new CompanyInfoSetup();
            win.ShowDialog();
            companyname.DataContext = helper.ExecuteScalar("SELECT name FROM companyinfo");
        }

        private void TransactionLog_Click(object sender, RoutedEventArgs e)
        {
            CompanyInfoSetup win = new CompanyInfoSetup();
            win.ShowDialog();
            companyname.DataContext = helper.ExecuteScalar("SELECT name FROM companyinfo");
        }

        private void CompanyInfo_Click(object sender, RoutedEventArgs e)
        {
            CompanyInfoSetup win = new CompanyInfoSetup();
            win.ShowDialog();
            companyname.DataContext = helper.ExecuteScalar("SELECT name FROM companyinfo");
        }

        
    }
}
