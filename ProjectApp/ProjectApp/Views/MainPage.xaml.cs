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
    public partial class MainPage : ContentPage
    {
        ArduinoHandler arduinoHandler = ArduinoHandler.Handler;

        // Used to refresh the GUI
        const double refreshIntervalMilliseconds = 1000;
        bool waitForResponse = false;

        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        ///  Use this method for things that need to be done to refresh the GUI everytime the timer goes off. 
        /// </summary>
        private void RefreshGUI()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (arduinoHandler.IsConnected())
                {
                    // DoorStatus.Text = arduinoHandler.GetDoorStatus(); 
                    ButtonConnect.IsEnabled = false;

                    if (waitForResponse)
                    {
                        if (arduinoHandler.CheckForDoorBell())
                        {
                            TextErrors.Text = Connection.counter.ToString();
                        }
                    }
                }
                else
                {
                    ButtonConnect.IsEnabled = true;
                }

            });
        }

        /// <summary>
        /// Event handler for when the "Connect" button is tapped.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectClicked(object sender, EventArgs e)
        {
            // Dont want to connect if already connected 
            if (arduinoHandler.IsConnected())
                return;

            string ipAddress = EntryIPAddress.Text;
            string port = EntryPort.Text;

            // Prevent user from pressing connect multiple times
            ButtonConnect.IsEnabled = false;

            // If succesfully connected, start the refresh GUI timer
            if (arduinoHandler.StartConnection(ipAddress, port))
            {
                Timer timer = new Timer(refreshIntervalMilliseconds);
                timer.Elapsed += (obj, args) => RefreshGUI();
                timer.Start();
            }
            else
            {
                ButtonConnect.IsEnabled = true;
                TextErrors.Text = "Looks like we had a problem connecting";
            }

            //Start with checking for the doorbell
            if (arduinoHandler.IsConnected())
                waitForResponse = true;

            // Refresh GUI (in case it needs to display errors)
            RefreshGUI();
        }
    }
}
