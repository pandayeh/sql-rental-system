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
    /// Interaction logic for Type_Setup.xaml
    /// </summary>
    public partial class TypeSetup : Window
    {
        private DataTable table;
        private SQLiteHelper helper;

        public TypeSetup()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();

            helper = new SQLiteHelper();
            table = helper.GetDataTable(
                "SELECT typeId, name " +
                "FROM types");
            gridTypes.DataContext = table.DefaultView;
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            NewType win = new NewType();
            win.ShowDialog();
            table = helper.GetDataTable(
                "SELECT typeId, name " +
                "FROM types");
            gridTypes.DataContext = table.DefaultView;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            EditType win = new EditType();
            win.ShowDialog();
            table = helper.GetDataTable(
                "SELECT typeId, name " +
                "FROM types");
            gridTypes.DataContext = table.DefaultView;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DataTable rented = helper.GetDataTable( //Check if anything with this type is rented out
                "SELECT rented.* " + 
                "FROM rented " + 
                "JOIN serials USING serialId " + 
                "JOIN series USING seriesId " + 
                "WHERE series.typeId=" + (int)gridTypes.SelectedValue);

            if (rented.Rows.Count != 0)
                MessageBox.Show("Error: There exist items of this type with active rentals.");
            else
            {
                if (MessageBox.Show(
                    "Are you sure you want to delete type \"" + ((DataRowView)gridTypes.SelectedItems[0])["name"] + "\"?\nAll items of this type will be deleted!",
                    "Confirm Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    helper.ExecuteNonQuery("DELETE FROM types WHERE typeId=" + gridTypes.SelectedValue);
                    table = helper.GetDataTable(
                        "SELECT typeId, name " +
                        "FROM types");
                    gridTypes.DataContext = table.DefaultView;
                }

            }

            table = helper.GetDataTable(
                "SELECT typeId, name " +
                "FROM types");
            gridTypes.DataContext = table.DefaultView;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
