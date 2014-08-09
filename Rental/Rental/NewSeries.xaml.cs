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
    /// Interaction logic for NewItem.xaml
    /// </summary>
    public partial class NewSeries : Window
    {
        private SQLiteHelper helper;
        private DataTable table;

        public NewSeries()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();

            helper = new SQLiteHelper();

            table = helper.GetDataTable( //TODO: Implement volume sets
                "SELECT typeId, name " +
                "FROM types");
            type.DataContext = table;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            string sql =
                "INSERT INTO customers (title,typeId,author,publisher,reference,defaultPrice,finished) " +
                "VALUES (\"" +
                title.Text + "\", \"" +
                type.SelectedValue + "\", \"" +
                author.Text + "\", \"" +
                publisher.Text + "\", \"" +
                reference.Text + "\", \"" +
                defaultPrice.Text + "\", \"" +
                ( ((bool)finished.IsChecked) ? "TRUE" : "FALSE" ) + "\")";

            helper.ExecuteNonQuery(sql);

            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
