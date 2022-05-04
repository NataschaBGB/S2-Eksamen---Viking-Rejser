using System;
using System.Collections.Generic;
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

namespace VikingRejser
{
    /// <summary>
    /// Interaction logic for UpdateCustomerPage.xaml
    /// </summary>
    public partial class UpdateCustomerPage : Window
    {
        // int to tell which id is selected to update customer
        int id;
        public UpdateCustomerPage(int updateId)
        {
            id = updateId;
            InitializeComponent();
            // loading existing customer info
            LoadCustomerInfo();
        }
        // method to load existing info on the selected customer
        public void LoadCustomerInfo()
        {
            // sets textboxes to contain name/adress when updatePage is opened
            boxCustomerNameUpd.Text = (MainWindow.datagridC.SelectedItem as Kunde).name;
            boxCustomerAdressUpd.Text = (MainWindow.datagridC.SelectedItem as Kunde).adress;
        }

        // update customer info
        private void BtnUpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new VikingRejserDBEntities())
            {
                // Single() returns the only element in a sequence that corresponds to a specific condition -
                // - in our case its the id
                var result = db.Kundes.Single(k => k.id == id);
                // if id is not null ( if it is clicked ) -
                if (result != null)
                {
                    // - set name/adress etc to text input in the textboxes on updatePage
                    result.name = boxCustomerNameUpd.Text.Trim();
                    result.adress = boxCustomerAdressUpd.Text.Trim();
                    result.phone = Convert.ToInt32(boxCustomerPhoneUpd.Text.Trim());
                    db.SaveChanges();
                }
                MainWindow.datagridC.ItemsSource = db.Kundes.ToList();
                // hide UpdateCustomerPage
                this.Hide();
                MessageBox.Show("Kunden er opdateret!");
            }
        }
    }
}
