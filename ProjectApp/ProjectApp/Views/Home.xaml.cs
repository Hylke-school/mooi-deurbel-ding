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
    public partial class Home : ContentPage
    {
        ArduinoHandler arduinoHandler = ArduinoHandler.Handler;

        public Home()
        {
            InitializeComponent();

            // Set GUI binding and register StatusRefreshedEvent event listener
            BindingContext = arduinoHandler.Status;
            arduinoHandler.StatusRefreshedEvent += RefreshGUI;
        }

        /// <summary>
        /// Event handler that gets fired everytime the ArduinoHandler periodically refreshes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RefreshGUI(object sender, EventArgs e)
        {

            if (arduinoHandler.IsConnected())
            {
                // Only enable the Unlock button when the box is actually closed
                ButtonUnlock.IsEnabled = arduinoHandler.Status.BoxStatus == "Closed";
            }

            else
            {
                ButtonUnlock.IsEnabled  = false;
            }
        }

        /// <summary>
        /// Event handler for when the unlock button is tapped.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonUnlock_Clicked(object sender, EventArgs e)
        {
            ButtonUnlock.IsEnabled = false;
            arduinoHandler.UnlockPackageBox();
        }
    }
}