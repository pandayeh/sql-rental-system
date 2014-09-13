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
            try
            {
                EditType win = new EditType(Convert.ToInt32(gridTypes.SelectedValue));
                win.ShowDialog();
                table = helper.GetDataTable(
                    "SELECT typeId, name " +
                    "FROM types");
                gridTypes.DataContext = table.DefaultView;
            }
            catch (IndexOutOfRangeException oops) { } //Click w/o any selection
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable rented = helper.GetDataTable( //Check if anything with this type is rented out
                            "SELECT rented.* " +
                            "FROM rented " +
                            "JOIN serials USING (serialId) " +
                            "JOIN series USING (seriesId) " +
                            "WHERE series.typeId=" + Convert.ToInt32(gridTypes.SelectedValue));

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
            }
            catch (IndexOutOfRangeException oops) { } //Click w/o any selection
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Select_Type(object sender, RoutedEventArgs e)
        {
            try
            {
                if (gridTypes.SelectedValue == null) //Closing a dialogue selects a row before the table can update
                    throw new IndexOutOfRangeException();

                DataTable vals = helper.GetDataTable(
                    "SELECT rentMultiplier,rentValue,hotMultiplier,hotValue,houseMultiplier,houseValue,depositMultiplier,depositValue,membershipMultiplier,membershipValue,overdueMultiplier,overdueValue " +
                    "FROM types WHERE typeId=" + gridTypes.SelectedValue);
                object[] items = vals.Rows[0].ItemArray;

                rentRate.DataContext = "Regular Rate: " + (((bool)items[0]) ? "x" : "+") + items[1].ToString();
                hotRate.DataContext = "Hot Rate: " + (((bool)items[2]) ? "x" : "+") + items[3].ToString();
                houseRate.DataContext = "In-Store Rate: " + (((bool)items[4]) ? "x" : "+") + items[5].ToString();
                depositRate.DataContext = "Deposit Rate: " + (((bool)items[6]) ? "x" : "+") + items[7].ToString();
                membershipRate.DataContext = "Membership Rate: " + (((bool)items[8]) ? "x" : "+") + items[9].ToString();
                overdueRate.DataContext = "Overdue Rate: " + (((bool)items[10]) ? "x" : "+") + items[11].ToString();
            }
            catch (IndexOutOfRangeException oops) {  } //New Type selects new row before table can update, causing this exception
        }
    }
}
