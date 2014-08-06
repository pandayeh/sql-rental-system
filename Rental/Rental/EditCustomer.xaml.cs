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
            name.Text = oldVals.Rows[0].ItemArray[0].ToString();
            code.Text = oldVals.Rows[0].ItemArray[1].ToString();
            email.Text = oldVals.Rows[0].ItemArray[2].ToString();
            id.Text = oldVals.Rows[0].ItemArray[3].ToString();
            phone.Text = oldVals.Rows[0].ItemArray[4].ToString();
            note.Text = oldVals.Rows[0].ItemArray[5].ToString();
            membership.SelectedIndex = Convert.ToInt32(oldVals.Rows[0].ItemArray[6]);
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            string sql = 
                "UPDATE customers (name,code,membershipId,id,email,phone,note) " +
                "SET "+
                "name=\"" + name.Text + "\", \"" +
                "code=\"" + code.Text + "\", \"" +
                "membership=\"" + membership.SelectedValue + "\", \"" +
                "id=\"" + id.Text + "\", \"" +
                "email=\"" + email.Text + "\", \"" +
                "phone=\"" + phone.Text + "\", \"" +
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
