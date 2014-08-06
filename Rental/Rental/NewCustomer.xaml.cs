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
    /// Interaction logic for NewCustomer.xaml
    /// </summary>
    public partial class NewCustomer : Window
    {
        private SQLiteHelper helper;
        private DataTable table;

        public NewCustomer()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();

            helper = new SQLiteHelper();

            table = helper.GetDataTable( //TODO: Implement volume sets
                "SELECT memberships.name " +
                "FROM memberships)");
            membership.DataContext = table.DefaultView;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            string sql1 = "INSERT INTO customers (name,code,membershipId";
            string sql2 = "VALUES (" + name.Text + ", " + code.Text + ", " + membership.SelectedIndex;

            if (id.Text != "")
            {
                sql1 += ", id";
                sql2 += ", " + id.Text;
            }
            if (email.Text != "")
            {
                sql1 += ", email";
                sql2 += ", " + email.Text;
            }
            if (phone.Text != "")
            {
                sql1 += ", phone";
                sql2 += ", " + phone.Text;
            }
            if (note.Text != "")
            {
                sql1 += ", note";
                sql2 += ", " + note.Text;
            }

            helper.ExecuteNonQuery(sql1 + ") " + sql2 + ")");
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
