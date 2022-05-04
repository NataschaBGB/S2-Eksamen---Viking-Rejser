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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VikingRejser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // tells the program to use this db
        VikingRejserDBEntities db = new VikingRejserDBEntities();
        // datagrids to connect this code to datagrid in xaml
        public static DataGrid datagridC;
        public static DataGrid datagridTravel;
        public static DataGrid datagridTransporters;
        

        public MainWindow()
        {
            InitializeComponent();

            // loading db info from our methods
            LoadCustomers();
            LoadTravels();
            LoadTransporters();
        }

        // methods to load info from the different tables in our db
        public void LoadCustomers()
        {
            // dgCustomers (dg in mainwindow) gets info from Kundes db
            dgCustomers.ItemsSource = db.Kundes.ToList();
            datagridC = dgCustomers;
        }

        public void LoadTravels()
        {
            dgTravels.ItemsSource = db.Rejsearrangements.ToList();
            datagridTravel = dgTravels;
        }
        public void LoadTransporters()
        {
            dgTransporters.ItemsSource = db.Transportør.ToList();
            datagridTransporters = dgTransporters;
        }

        // method to clear all textbox's
        public void Clear()
        {
            boxCustomerName.Text = boxCustomerAdress.Text = boxCustomerPhone.Text = "";
            boxTravelTitle.Text = boxTravelCity.Text = dpTravelStart.Text = dpTravelEnd.Text = boxTravelPrice.Text = boxTravelMax.Text = boxTravelTrans.Text = boxTravelDescription.Text = "";
            boxTransName.Text = boxTransAdress.Text = boxTransPhone.Text = boxTransNotes.Text = "";
        }

        // closes the window
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // create new customer in db
        private void BtnCreateCustomer_Click(object sender, RoutedEventArgs e)
        {
            Kunde AddCustomer = new Kunde()
            {
                // sets db name/adress/phone to text input in textbox - Trim() to clear white space
                name = boxCustomerName.Text.Trim(),
                adress = boxCustomerAdress.Text.Trim(),
                phone = Convert.ToInt32(boxCustomerPhone.Text.Trim())
            };

            // if the input phone number matches an existing phone number in the db -
            if ( boxCustomerPhone.Text == (MainWindow.datagridC.SelectedItem as Kunde).phone.ToString())
            {
                // clear the textbox for number and show this message
                boxCustomerPhone.Clear();
                MessageBox.Show("En Kunde med samme telefonnummer er allerede oprettet i systemet!");
            }
            else
            {
                // if the phone number doesnt exist in the db, add to db, save changes and update dg
                db.Kundes.Add(AddCustomer);
                db.SaveChanges();
                // update the dg to show the newly added item
                datagridC.ItemsSource = db.Kundes.ToList();
                // calling the Clear method to clear all text in textbox - ready for a new item to be created
                Clear();
                MessageBox.Show("Kunden er oprettet!");
            }

        }
        // update existing customer
        private void BtnUpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            // int that matches the id in db
            int id = (datagridC.SelectedItem as Kunde).id;
            // show update page for the selected customer (the selected id)
            UpdateCustomerPage update = new UpdateCustomerPage(id);
            update.ShowDialog();
        }

        // create trip info
        private void BtnCreateTravel_Click(object sender, RoutedEventArgs e)
        {
            Rejsearrangement AddTravel = new Rejsearrangement()
            {
                // sets title/city etc. to text input in textbox - Trim() to clear white space
                title = boxTravelTitle.Text.Trim(),
                city = boxTravelCity.Text.Trim(),
                startDate = Convert.ToDateTime(dpTravelStart.Text.Trim()),
                endDate = Convert.ToDateTime(dpTravelEnd.Text.Trim()),
                price = Convert.ToInt32(boxTravelPrice.Text.Trim()),
                maxPeople = Convert.ToInt32(boxTravelMax.Text.Trim()),
                transporter = boxTravelTrans.Text.Trim(),
                description = boxTravelDescription.Text.Trim(),
                signedUp = Convert.ToInt32(boxTravelSignedUp.Text.Trim())
            };
            
            // adds the newly created trip info to the db table
            db.Rejsearrangements.Add(AddTravel);
            db.SaveChanges();
            // updates the dg to show the newly added item
            datagridTravel.ItemsSource = db.Rejsearrangements.ToList();
            Clear();
            MessageBox.Show("Rejsen er oprettet!");
        }
        // update existing travel
        private void BtnUpdateTravel_Click(object sender, RoutedEventArgs e)
        {
            int id = (datagridTravel.SelectedItem as Rejsearrangement).id;
            UpdateTravelPage updateTrip = new UpdateTravelPage(id);
            updateTrip.ShowDialog();
        }

        // create new transporter
        private void BtnCreateTrans_Click(object sender, RoutedEventArgs e)
        {
            Transportør AddTransporter = new Transportør()
            {
                // set Transportør table column names to match users text in textbox
                name = boxTransName.Text.Trim(),
                adress = boxTransAdress.Text.Trim(),
                phone = Convert.ToInt32(boxTransPhone.Text.Trim()),
                notes = boxTransNotes.Text.Trim()
            };

            // if the input phone number matches an existing phone number in the db -
            if (boxTransPhone.Text == (MainWindow.datagridTransporters.SelectedItem as Transportør).phone.ToString())
            {
                // - clear the phone textbox and show this message
                boxTransPhone.Clear();
                MessageBox.Show("En Transportør med samme telefonnummer er allerede oprettet i systemet!");
            }
            else
            {
                // if the phone number doesnt exist in the db, add to db, save changes and update dg
                db.Transportør.Add(AddTransporter);
                db.SaveChanges();
                datagridTransporters.ItemsSource = db.Transportør.ToList();
                Clear();
                MessageBox.Show("Transportøren er oprettet!");

            }
        }
        // update existing transporter
        private void BtnUpdateTrans_Click(object sender, RoutedEventArgs e)
        {
            int id = (datagridTransporters.SelectedItem as Transportør).id;
            UpdateTransPage updateTransporter = new UpdateTransPage(id);
            updateTransporter.ShowDialog();
        }

    }
}