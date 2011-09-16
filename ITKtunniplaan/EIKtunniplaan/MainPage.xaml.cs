using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Globalization;

namespace EIKtunniplaan
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
                                //käespoleva nädala nr, vaja muuta interaktiivseks
                                // Gets the Calendar instance associated with a CultureInfo.
                                CultureInfo myCI = CultureInfo.CurrentCulture;
                                Calendar myCal = myCI.Calendar;

                                // Gets the DTFI properties required by GetWeekOfYear.
                                CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
                                DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;

                                // Displays the number of the current week relative to the beginning of the year.
                                //Console.WriteLine("The CalendarWeekRule used for the en-US culture is {0}.", myCWR);
                                //Console.WriteLine("The FirstDayOfWeek used for the en-US culture is {0}.", myFirstDOW);
                                int nr = myCal.GetWeekOfYear(DateTime.Now, myCWR, myFirstDOW);

            this.nadal.Text = nr.ToString();
            App.ViewModel.sisestatudNadalaNr = Convert.ToInt32(this.nadal.Text);
            Uri gameUri = new Uri("/Nadal.xaml", UriKind.Relative); NavigationService.Navigate(gameUri);
        }
    }
}