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
    /// Interaction logic for EditType.xaml
    /// </summary>
    public partial class EditType : Window
    {
        private SQLiteHelper helper;
        private int typeId;

        public EditType(int tid)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();

            helper = new SQLiteHelper();
            typeId = tid;

            DataTable oldVals = helper.GetDataTable("SELECT * FROM types WHERE typeId="+typeId);
            object[] items = oldVals.Rows[0].ItemArray;
            name.Text = items[1].ToString();
            if ((bool)items[2])
            {
                rentMulti.Text = items[3].ToString();
                rentMultiplier.IsChecked = true;
            }
            else
            {
                rentFlat.Text = items[3].ToString();
                rentFlatiplier.IsChecked = true;
            }
            rentQuantity.Text = items[4].ToString();
            rentDays.Text = items[5].ToString();

            if ((bool)items[6])
            {
                hotMulti.Text = items[7].ToString();
                hotMultiplier.IsChecked = true;
            }
            else
            {
                hotFlat.Text = items[7].ToString();
                hotFlatiplier.IsChecked = true;
            }
            hotQuantity.Text = items[8].ToString();
            hotDays.Text = items[9].ToString();
            hotLength.Text = items[10].ToString();

            if ((bool)items[11])
            {
                houseMulti.Text = items[12].ToString();
                houseMultiplier.IsChecked = true;
            }
            else
            {
                houseFlat.Text = items[12].ToString();
                houseFlatiplier.IsChecked = true;
            }

            if ((bool)items[13])
            {
                depositMulti.Text = items[14].ToString();
                depositMultiplier.IsChecked = true;
            }
            else
            {
                depositFlat.Text = items[14].ToString();
                depositFlatiplier.IsChecked = true;
            }

            if ((bool)items[15])
            {
                membershipMulti.Text = items[16].ToString();
                membershipMultiplier.IsChecked = true;
            }
            else
            {
                membershipFlat.Text = items[16].ToString();
                membershipFlatiplier.IsChecked = true;
            }

            if ((bool)items[17])
            {
                overdueMulti.Text = items[18].ToString();
                overdueMultiplier.IsChecked = true;
            }
            else
            {
                overdueFlat.Text = items[18].ToString();
                overdueFlatiplier.IsChecked = true;
            }
            overdueDays.Text = items[19].ToString();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            string checking = 
                (((bool)rentMultiplier.IsChecked) ? rentMulti.Text : rentFlat.Text) + ", " +
                rentQuantity.Text + "," +
                rentDays.Text + "," + 

                (((bool)hotMultiplier.IsChecked) ? hotMulti.Text : hotFlat.Text) + "," +
                hotQuantity.Text + "," +
                hotDays.Text + "," +
                hotLength.Text + "," +

                (((bool)houseMultiplier.IsChecked) ? houseMulti.Text : houseFlat.Text) + "," +

                (((bool)depositMultiplier.IsChecked) ? depositMulti.Text : depositFlat.Text) + "," +

                (((bool)overdueMultiplier.IsChecked) ? overdueMulti.Text : overdueFlat.Text) + "," +
                overdueDays.Text;

            string[] checklist = checking.Split(",".ToCharArray());
            bool success = true;

            foreach(string s in checklist)
            {
                if (s != "")
                {
                    try
                    {
                        Convert.ToDouble(s);
                    }
                    catch (Exception oops)
                    {
                        MessageBox.Show("Rates may only contain numbers and decimals.");
                        success = false;
                        break;
                    }
                }
            }

            if (success)
            {
                string sql =
                    "UPDATE types " +
                    "SET " +
                    "name=\"" + name.Text + "\", " +

                    "rentMultiplier=\"" + (((bool)rentMultiplier.IsChecked) ? 1 : 0) + "\", " +
                    "rentValue=\"" + (((bool)rentMultiplier.IsChecked) ? rentMulti.Text : rentFlat.Text) + "\", " +
                    "rentQuantity=\"" + rentQuantity.Text + "\", " +
                    "rentDays=\"" + rentDays.Text + "\", " +

                    "hotMultiplier=\"" + (((bool)hotMultiplier.IsChecked) ? 1 : 0) + "\", " +
                    "hotValue=\"" + (((bool)hotMultiplier.IsChecked) ? hotMulti.Text : hotFlat.Text) + "\", " +
                    "hotQuantity=\"" + hotQuantity.Text + "\", " +
                    "hotDays=\"" + hotDays.Text + "\", " +
                    "hotLength=\"" + hotLength.Text + "\", " +

                    "houseMultiplier=\"" + (((bool)houseMultiplier.IsChecked) ? 1 : 0) + "\", " +
                    "houseValue=\"" + (((bool)houseMultiplier.IsChecked) ? houseMulti.Text : houseFlat.Text) + "\", " +

                    "depositMultiplier=\"" + (((bool)depositMultiplier.IsChecked) ? 1 : 0) + "\", " +
                    "depositValue=\"" + (((bool)depositMultiplier.IsChecked) ? depositMulti.Text : depositFlat.Text) + "\", " +

                    "membershipMultiplier=\"" + (((bool)membershipMultiplier.IsChecked) ? 1 : 0) + "\", " +
                    "membershipValue=\"" + (((bool)membershipMultiplier.IsChecked) ? membershipMulti.Text : membershipFlat.Text) + "\", " +

                    "overdueMultiplier=\"" + (((bool)overdueMultiplier.IsChecked) ? 1 : 0) + "\", " +
                    "overdueValue=\"" + (((bool)overdueMultiplier.IsChecked) ? overdueMulti.Text : overdueFlat.Text) + "\", " +
                    "overdueDays=\"" + overdueDays.Text + "\" " +

                    "WHERE typeId=" + typeId;

                helper.ExecuteNonQuery(sql);
                Close();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
