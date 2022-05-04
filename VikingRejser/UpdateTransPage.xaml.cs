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
    /// Interaction logic for UpdateTransPage.xaml
    /// </summary>
    public partial class UpdateTransPage : Window
    {
        // int to tell which id is selected to update Transporter
        int id;
        public UpdateTransPage(int updateId)
        {
            InitializeComponent();
            // loading existing transporter info
            LoadTransporterInfo();
            id = updateId;
        }

        // method to load existing info on the selected transporter
        public void LoadTransporterInfo()
        {
            // sets textboxes to contain name/adress etc when updatePage is opened
            boxTransNameUpd.Text = (MainWindow.datagridTransporters.SelectedItem as Transportør).name;
            boxTransAdressUpd.Text = (MainWindow.datagridTransporters.SelectedItem as Transportør).adress;
            boxTransPhoneUpd.Text = (MainWindow.datagridTransporters.SelectedItem as Transportør).phone.ToString();
            boxTransNotesUpd.Text = (MainWindow.datagridTransporters.SelectedItem as Transportør).notes;
        }

        // update transporter info
        private void BtnUpdateTrans_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new VikingRejserDBEntities())
            {
                // Single() returns the only element in a sequence that corresponds to a specific condition -
                // - in our case its the id
                var result = db.Transportør.SingleOrDefault(t => t.id == id);
                // if id is not null ( if it is clicked ) -
                if (result != null)
                {
                    // - set name/adress etc to text input in the textboxes on updatePage
                    result.name = boxTransNameUpd.Text.Trim();
                    result.adress = boxTransAdressUpd.Text.Trim();
                    result.phone = Convert.ToInt32(boxTransPhoneUpd.Text.Trim());
                    result.notes = boxTransNotesUpd.Text.Trim();
                    db.SaveChanges();
                }
                MainWindow.datagridTransporters.ItemsSource = db.Transportør.ToList();
                // hide UpdateTransPage
                this.Hide();
                MessageBox.Show("Transportøren er opdateret!");
            }
        }
    }
}
