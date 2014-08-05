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
    /// Interaction logic for EnterCustomerCode.xaml
    /// </summary>
    public partial class EnterCustomerCodeRent : Window
    {
        public EnterCustomerCodeRent()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            ClickHelper("customers.code");
        }

        private void Name_Click(object sender, RoutedEventArgs e)
        {
            
            ClickHelper("customers.name");
        }

        public void ClickHelper(string condition)
        {
            SQLiteHelper helper = new SQLiteHelper();
            DataTable table = helper.GetDataTable( //TODO: Implement volume sets
                "SELECT customers.customerId, customers.code AS Code, customers.name AS Name " +
                "FROM customers " +
                "WHERE "+condition+" LIKE '%" + code.Text + "%'");
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

            if(count != 0)
                Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
