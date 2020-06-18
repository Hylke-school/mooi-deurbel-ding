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

        // Used to refresh the GUI
        const double refreshIntervalMilliseconds = 1000;
        bool waitForResponse = false;

        public Home()
        {
            InitializeComponent();
            arduinoHandler.IsConnected();

            this.BindingContext = arduinoHandler.Status;
        }

        private void ButtonUnlock_Clicked(object sender, EventArgs e)
        {
            arduinoHandler.UnlockPackageBox();

        }

        private void ButtonLock_Clicked(object sender, EventArgs e)
        {
            arduinoHandler.LockPackageBox();
        }
    }
}