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
    /// Interaction logic for NewType.xaml
    /// </summary>
    public partial class NewType : Window
    {
        private SQLiteHelper helper;

        public NewType()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();

            helper = new SQLiteHelper();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            string sql1 =
                "INSERT INTO types (name," +
                "rentMultiplier,rentValue,rentQuantity,rentDays," +
                "hotMultiplier,hotValue,hotQuantity,hotDays,hotLength," +
                "houseMultiplier,houseValue," +
                "depositMultiplier,depositValue," +
                "membershipMultiplier,membershipValue," +
                "overdueMultiplier,overdueValue,overdueDays ) " +

                "VALUES (\"" +
                name.Text + "\", \"";

            string sql2 = 
                rentMultiplier.IsChecked + "\", \"" +
                (((bool)rentMultiplier.IsChecked) ? rentMulti.Text : rentFlat.Text) + "\", \"" +
                rentQuantity.Text + "\", \"" +
                rentDays.Text + "\", \"" +

                hotMultiplier.IsChecked + "\", \"" +
                (((bool)hotMultiplier.IsChecked) ? hotMulti.Text : hotFlat.Text) + "\", \"" +
                hotQuantity.Text + "\", \"" +
                hotDays.Text + "\", \"" +
                hotLength.Text + "\", \"" +

                houseMultiplier.IsChecked + "\", \"" +
                (((bool)houseMultiplier.IsChecked) ? houseMulti.Text : houseFlat.Text) + "\", \"" +

                depositMultiplier.IsChecked + "\", \"" +
                (((bool)depositMultiplier.IsChecked) ? depositMulti.Text : depositFlat.Text) + "\", \"" +

                membershipMultiplier.IsChecked + "\", \"" +
                (((bool)membershipMultiplier.IsChecked) ? membershipMulti.Text : membershipFlat.Text) + "\", \"" +

                overdueMultiplier.IsChecked + "\", \"" +
                (((bool)overdueMultiplier.IsChecked) ? overdueMulti.Text : overdueFlat.Text) + "\", \"" +
                overdueDays.Text + "\")";


            string temp2 = sql2.Replace("True","").Replace("False","").Replace("\")",""); //Removes all booleans
            char[] delim = "\", \"".ToCharArray();
            string[] temp = temp2.Split(delim);
            bool success = true;

            foreach(string s in temp) //Check if each input is valid
            {
                if(s != "")
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

            if(success)
            {
                helper.ExecuteNonQuery(sql1 + sql2);
                Close();
            }
            
            

            

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
