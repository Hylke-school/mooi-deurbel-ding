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
using Application = Xamarin.Forms.Application;

namespace ProjectApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class Settings : ContentPage
    {
        ArduinoHandler arduinoHandler = ArduinoHandler.Handler;

        public Settings()
        {
            InitializeComponent();
            arduinoHandler.StatusRefreshedEvent += RefreshGUI;

            if (Application.Current.Properties.ContainsKey("IP"))
                EntryIPAddress.Text = Application.Current.Properties["IP"] as string;

            if (Application.Current.Properties.ContainsKey("PORT"))
                EntryPort.Text = Application.Current.Properties["PORT"] as string;
        }

        /// <summary>
        ///  Event handler that gets fired everytime the ArduinoHandler periodically refreshes.
        /// </summary>
        private void RefreshGUI(object sender, EventArgs e)
        {
            // Enable / disable connect buttons depending on the connection status
            ButtonConnect.IsEnabled = !arduinoHandler.IsConnected();
            ButtonDisconnect.IsEnabled = arduinoHandler.IsConnected();
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

            Application.Current.Properties["IP"] = ipAddress;
            Application.Current.Properties["PORT"] = port;
            Application.Current.SavePropertiesAsync();

            // Prevent user from pressing connect multiple times
            ButtonConnect.IsEnabled = false;

            // See if succesful connection could be made
            if (arduinoHandler.StartConnection(ipAddress, port) == false)
            {
                ButtonConnect.IsEnabled = true;
                TextErrors.Text = "Looks like we had a problem connecting";
            }
        }

        /// <summary>
        /// Event handler for when the Disconnect button is tapped. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisconnectClicked(object sender, EventArgs e)
        {
            ButtonDisconnect.IsEnabled = false;
            arduinoHandler.CloseConnection();
        }
    }
}