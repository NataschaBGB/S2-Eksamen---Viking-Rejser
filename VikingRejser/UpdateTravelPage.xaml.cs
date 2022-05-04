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
    /// Interaction logic for UpdateTravelPage.xaml
    /// </summary>
    public partial class UpdateTravelPage : Window
    {
        // int to tell which id is selected to update trip
        int id;
        public UpdateTravelPage(int updateId)
        {
            InitializeComponent();
            // loading existing trip info
            LoadTravelInfo();
            id = updateId;
        }
        // method to load existing info on the selected trip
        public void LoadTravelInfo()
        {
            // sets textboxes to contain title/city etc when updatePage is opened
            boxTravelTitleUpd.Text = (MainWindow.datagridTravel.SelectedItem as Rejsearrangement).title;
            boxTravelCityUpd.Text = (MainWindow.datagridTravel.SelectedItem as Rejsearrangement).city;
            dpTravelStartUpd.Text = (MainWindow.datagridTravel.SelectedItem as Rejsearrangement).startDate.ToString();
            dpTravelEndUpd.Text = (MainWindow.datagridTravel.SelectedItem as Rejsearrangement).endDate.ToString();
            boxTravelMaxUpd.Text = (MainWindow.datagridTravel.SelectedItem as Rejsearrangement).maxPeople.ToString();
            boxTravelTransUpd.Text = (MainWindow.datagridTravel.SelectedItem as Rejsearrangement).transporter;
            boxTravelPriceUpd.Text = (MainWindow.datagridTravel.SelectedItem as Rejsearrangement).price.ToString();
            boxTravelDescriptionUpd.Text = (MainWindow.datagridTravel.SelectedItem as Rejsearrangement).description;
            boxTravelSignedUpd.Text = (MainWindow.datagridTravel.SelectedItem as Rejsearrangement).signedUp.ToString();
        }

        // update trip info
        private void BtnUpdateTravel_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new VikingRejserDBEntities())
            {
                // Single() returns the only element in a sequence that corresponds to a specific condition -
                // - in our case its the id
                var result = db.Rejsearrangements.SingleOrDefault(k => k.id == id);
                // if id is not null ( if it is clicked ) -
                if (result != null)
                {
                    // - set name/adress etc to text input in the textboxes on updatePage
                    result.title = boxTravelTitleUpd.Text.Trim();
                    result.city = boxTravelCityUpd.Text.Trim();
                    result.startDate = Convert.ToDateTime(dpTravelStartUpd.Text.Trim());
                    result.endDate = Convert.ToDateTime(dpTravelEndUpd.Text.Trim());
                    result.price = Convert.ToInt32(boxTravelPriceUpd.Text.Trim());
                    result.maxPeople = Convert.ToInt32(boxTravelMaxUpd.Text.Trim());
                    result.transporter = boxTravelTransUpd.Text.Trim();
                    result.description = boxTravelDescriptionUpd.Text.Trim();
                    result.signedUp = Convert.ToInt32(boxTravelSignedUpd.Text.Trim());
                    db.SaveChanges();
                }
                MainWindow.datagridTravel.ItemsSource = db.Rejsearrangements.ToList();
                // hide UpdateTravelPage
                this.Hide();
                MessageBox.Show("Arrangementet er opdateret!");
            }
        }
    }
}