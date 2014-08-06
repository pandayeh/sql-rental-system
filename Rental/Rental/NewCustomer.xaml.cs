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
                "SELECT memberships.membershipId, memberships.name " +
                "FROM memberships");
            membership.DataContext = table;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            string sql = 
                "INSERT INTO customers (name,code,membershipId,id,email,phone,note) " +
                "VALUES (\"" + 
                name.Text + "\", \"" + 
                code.Text + "\", \"" + 
                membership.SelectedValue + "\", \"" + 
                id.Text + "\", \"" + 
                email.Text + "\", \"" + 
                phone.Text + "\", \"" + 
                note.Text + "\")";

            helper.ExecuteNonQuery(sql);

            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
