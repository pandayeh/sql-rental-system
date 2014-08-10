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
    /// Interaction logic for EditCustomer.xaml
    /// </summary>
    public partial class EditCustomer : Window
    {
        private SQLiteHelper helper;
        private DataTable table;
        private int customerId;

        public EditCustomer(int cid)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();

            helper = new SQLiteHelper();
            customerId = cid;

            table = helper.GetDataTable( //TODO: Implement volume sets
                "SELECT memberships.membershipId, memberships.name " +
                "FROM memberships");
            membership.DataContext = table;

            DataTable oldVals = helper.GetDataTable("SELECT name, code, email, id, phone, note, membershipId FROM customers WHERE customerId=" + customerId);
            object[] items = oldVals.Rows[0].ItemArray;
            name.Text = items[0].ToString();
            code.Text = items[1].ToString();
            email.Text = items[2].ToString();
            id.Text = items[3].ToString();
            phone.Text = items[4].ToString();
            note.Text = items[5].ToString();
            membership.SelectedIndex = Convert.ToInt32(items[6]);
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            string sql = 
                "UPDATE customers " +
                "SET "+
                "name=\"" + name.Text + "\", " +
                "code=\"" + code.Text + "\", " +
                "membershipId=\"" + membership.SelectedValue + "\", " +
                "id=\"" + id.Text + "\", " +
                "email=\"" + email.Text + "\", " +
                "phone=\"" + phone.Text + "\", " +
                "note=\"" + note.Text + "\" " +
                "WHERE customerId=" + customerId;

            helper.ExecuteNonQuery(sql);

            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
