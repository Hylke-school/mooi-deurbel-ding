using ProjectApp.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpdrachtA : ContentPage
    {
        ArduinoHandler arduinoHandler;

        // Used to refresh the GUI
        const double refreshIntervalMilliseconds = 1000;

        public OpdrachtA()
        {
            InitializeComponent();
            arduinoHandler = new ArduinoHandler();

            // Bind the text on screen to ArduinoHandler's status strings so it automatically updates when variables get changed
            //this.BindingContext = arduinoHandler.Status;
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
                    arduinoHandler.RefreshStatus();
                    TextSensorValue.Text = arduinoHandler.GetSensorValue();

                    ButtonConnect.IsEnabled = false;
                    ButtonSetCriteria.IsEnabled = true;
                    ButtonToggleCriteria.IsEnabled = true;
                }

                else
                {
                    ButtonConnect.IsEnabled = true;
                    ButtonSetCriteria.IsEnabled = false;
                    ButtonChangeState.IsEnabled = false;
                    ButtonToggleCriteria.IsEnabled = false;
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

        /// <summary>
        /// Event handler for when the "Change State" button is tapped.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeStateClicked(object sender, EventArgs e)
        {
            TextErrors.Text = "";

            // Execute command 
            if (arduinoHandler.TogglePinState() == "error")
            {
                TextErrors.Text = "Error: Could not send state change";
            }

            RefreshGUI();
        }

        /// <summary>
        /// Event handler for when the "set criteria" button is tapped.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetCriteriaClicked(object sender, EventArgs e)
        {
            TextErrors.Text = "";

            int sensorCriteria = Convert.ToInt32(SliderSensorCriteria.Value);

            if (arduinoHandler.SetSensorCriteria(sensorCriteria) == "error")
            {
                TextErrors.Text = "Error: Could not set sensor criteria";
            }

            RefreshGUI();
        }

        /// <summary>
        /// Event handler for when the "toggle criteria" button is tapped.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleCriteriaClicked(object sender, EventArgs e)
        {
            string newMode = "";

            // Toggle logic in GUI class, this is retarded but too late / too lazy to change now

            //if (arduinoHandler.Status.CriteriaMode == "auto")
            //    newMode = "m";

            //else if (arduinoHandler.Status.CriteriaMode == "manual")
            //    newMode = "a";

            // Something weird went wrong
            if (newMode == "")
                return;

            arduinoHandler.SetCriteriaMode(newMode);
        }

        /// <summary>
        /// Event handler for when the Sensor Criteria Slider value gets changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SliderSensorCriteriaChanged(object sender, ValueChangedEventArgs e)
        {
            int value = Convert.ToInt32(e.NewValue);
            TextSensorCriteriaSlider.Text = Convert.ToString(value);
        }
    }
}