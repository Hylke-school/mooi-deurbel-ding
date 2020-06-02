using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ProjectApp.Server
{
    /// <summary>
    /// A class / object to store server statuses in as Strings to display in the user interface.
    /// </summary>
    public class ArduinoStatus : INotifyPropertyChanged
    {

        /*
         * This class is used for data binding with the userface
         * It looks so weird to make sure that when data gets updated, the user interface sees the update and also automatically updates
         * Code example used from https://stackoverflow.com/questions/42124944/xamarin-forms-force-a-control-to-refresh-value-from-binding
         */

        private string sensorValue;

        /// <summary>
        /// The sensor value (probably a number).
        /// </summary>
        public string SensorValue
        {
            get { return sensorValue; }
            set
            {
                sensorValue = value;
                OnPropertyChanged(nameof(SensorValue));
            }
        }

        private string pinState;

        /// <summary>
        /// "On" or "Off", the state of the pin.
        /// </summary>
        public string PinState
        {
            get { return pinState; }
            set
            {
                pinState = value;
                OnPropertyChanged(nameof(PinState));
            }
        }

        private string connectionStatus;

        /// <summary>
        /// Connection status, "Connected" or "Not connected".
        /// </summary>
        public string ConnectionStatus
        {
            get { return connectionStatus; }
            set
            {
                connectionStatus = value;
                OnPropertyChanged(nameof(ConnectionStatus));
            }
        }

        /// <summary>
        /// When the sensor reaches this criteria (number), make the Arduino activate something.
        /// </summary>
        private string sensorCriteria;
        public string SensorCriteria
        {
            get { return sensorCriteria; }
            set
            {
                sensorCriteria = value;
                OnPropertyChanged(nameof(SensorCriteria));
            }
        }

        private string criteriaMode;

        /// <summary>
        /// Whether the Arduino changes state automatically based on SensorCriteria ("auto") or the user toggles it manually with the app ("manual").
        /// </summary>
        public string CriteriaMode
        {
            get { return criteriaMode; }
            set
            {
                criteriaMode = value;
                OnPropertyChanged(nameof(CriteriaMode));
            }
        }

        /// <summary>
        /// Info status text.
        /// </summary>
        private string infoText;
        public string InfoText
        {
            get { return infoText; }
            set
            {
                infoText = value;
                OnPropertyChanged(nameof(InfoText));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
