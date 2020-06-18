using ProjectApp.Database;
using ProjectApp.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace ProjectApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class History : ContentPage
    {
        List<string> dates;

        public History()
        {
            InitializeComponent();
            RefreshDates();
        }

        /// <summary>
        /// Event that gets fired when the page appears on the screen. Used to reload the dates. 
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshDates();
        }


        /// <summary>
        /// Event handler for when the clear history button is tapped.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClearTapped(object sender, EventArgs e)
        {
            DatabaseManager.DeleteDates();
            RefreshDates();
        }

        /// <summary>
        /// Refresh the list of dates on the screen.
        /// </summary>
        public void RefreshDates()
        {
            dates = new List<string>(DatabaseManager.GetHistoryDates());
            HistoryList.ItemsSource = dates;
        }


    }
}