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
        ArduinoHandler arduinoHandler;

        // Used to refresh the GUI
        const double refreshIntervalMilliseconds = 1000;

        public MainPage()
        {
            InitializeComponent();

            arduinoHandler = new ArduinoHandler();
        }

        /// <summary>
        /// Refresh the GUI. Mostly used to refresh the sensor value but it refreshes everything inside the ArduinoHandler.Status object. 
        /// </summary>
        private void RefreshGUI()
        {
            Device.BeginInvokeOnMainThread(() =>
            {

                if (arduinoHandler.IsConnected())
                {
                    // DoorStatus.Text = arduinoHandler.GetDoorStatus(); 
                    ButtonConnect.IsEnabled = false;
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

            // Refresh GUI (in case it needs to display errors)
            RefreshGUI();
        }

        int counter = 0;

        private void TestClicked(object sender, EventArgs e)
        {
            TextErrors.Text = "-";

            if (!arduinoHandler.IsConnected())
                return;

            if (arduinoHandler.SendSomething())
            {
                TextErrors.Text = "Ding Dong";
                counter++;
            }
            else
            {
                TextErrors.Text = "No Ding Dong";
            }
        }
        
        private void SecondClicked(object sender, EventArgs e)
        {
            ButtonTest.BackgroundColor = ButtonTest.BackgroundColor == Color.Green ? Color.Aqua : Color.Green;
        }
    }
}
