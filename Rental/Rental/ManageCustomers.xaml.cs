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
                "SELECT customers.code AS Code, customers.name AS Name, customers.phone AS Phone, customers.email AS E-mail " +
                "FROM customers");
            gridCustomers.DataContext = table.DefaultView;
        }

        private void search_KeyDown(object sender, KeyEventArgs e)
        {
            table = helper.GetDataTable( //TODO: select from datatable instead of database?
                "SELECT customers.code AS Code, customers.name AS Name, customers.phone AS Phone, customers.email AS E-mail " +
                "FROM customers " +
                "WHERE customers.code=" + search.Text + " OR customers.name=" + search.Text);
        }
    }
}
