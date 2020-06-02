using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Xamarin.Forms;

namespace ProjectApp.Server
{
    class ArduinoHandler
    {
        /// <summary>ArduinoStatus object that can be used as a DataBinding object for the user interface to automatically update and display data.</summary>
        public ArduinoStatus Status { get; private set; }

        private Connection connection;
        
        /// <summary>Returns an ArduinoHandler object. Invoke StartConnection() to start a connection with the server.</summary>
        public ArduinoHandler()
        {
            connection = new Connection();
            Status = new ArduinoStatus()
            {
                ConnectionStatus = "Not connected",
                PinState = "-",
                SensorValue = "-",
                SensorCriteria = "-",
                InfoText = "-",
                CriteriaMode = "-"
            };
        }

        /// <summary>
        /// Attempts to establish a connection with the Arduino server and returns whether it went succesful or not.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when: ipAddress is valid, port is invalid, or a connection could not be made.</exception>
        /// <param name="ipAddress">The IP address of the server.</param>
        /// <param name="port">The port of the server.</param>
        /// <returns></returns>
        public bool StartConnection(string ipAddress, string port)
        {
            if (connection.Connected == true)
                return false;

            // Try to start connection
            try
            {
                //Console.WriteLine("IP: " + ipAddress);
                //Console.WriteLine("PORT: " + port);

                // Start connection
                connection.StartConnection(ipAddress, port);

                Status.InfoText = "-";

                Device.InvokeOnMainThreadAsync(() =>
                {
                    Status.PinState = GetPinState();
                    Status.SensorCriteria = GetSensorCriteria();
                    Status.CriteriaMode = GetCriteriaMode();
                });
            }

            catch (ArgumentException e)
            {
                Status.InfoText = e.Message;
            }

            Status.ConnectionStatus = connection.Connected ? "Connected" : "Not connected";

            return Status.ConnectionStatus == "Connected";
        }

        /// <summary>
        /// Refreshes the status (connected / not connected, and the sensor value).
        /// </summary>
        public void RefreshStatus()
        {
            // Set connected 
            Status.ConnectionStatus = connection.Connected ? "Connected" : "Not connected";

            if (connection.Connected) {
                // Set sensor value (and info text)
                string sensorValue = GetSensorValue();

                if (sensorValue == "error")
                {
                    Status.InfoText = "Could not retrieve sensor data";
                    Status.SensorValue = "-";
                }

                else Status.SensorValue = sensorValue;

                if (Status.CriteriaMode == "auto")
                {
                    Status.PinState = GetPinState();
                }
            }          
        }

        /* =========================================================================
                                                      .___      
          ____  ____   _____   _____ _____    ____    __| _/______
        _/ ___\/  _ \ /     \ /     \\__  \  /    \  / __ |/  ___/
        \  \__(  <_> )  Y Y  \  Y Y  \/ __ \|   |  \/ /_/ |\___ \ 
         \___  >____/|__|_|  /__|_|  (____  /___|  /\____ /____  >
             \/            \/      \/     \/     \/      \/    \/ 

        Everything after this should be related to commands to send to the server.
        Make sure to provide very detailed comments of what the command is, and what the response should be,
        because the commands and responses should also be handled in the Arduino server code.
        The reason why commands each have their own methods is for safety and to make debugging easier. 
         */

        /* Command List - Put a list of commands here so it's easy to look up when coding the Arduino responses
         * Format: Method - Command - Expected Arduino response (NOT method return value)
         * 
         * GetSensorValue()     - "s" - Value
         * GetPinState()        - "p" - "ON", "OFF"
         * GetSensorCriteria()  - "c" - Sensor criteria number
         * GetCriteriaMode()    - "m" - "00a" (auto) or "00m" (manual)
         * 
         * TogglePinState()     - "t" - "ON" or "OFF" (the new state)
         * SetSensorCriteria()  - "s=X" where x is a number - "sOk"
         * SetCriteriaMode()    - "ma" or "mm" (for Mode Automatic, or Mode Manual) - "00m" or "00a" (the NEW mode)
         */



        /// <summary>
        /// Returns the value from the sensor, or "error" if it could not be obtained.
        /// </summary>
        /// <returns></returns>
        public string GetSensorValue()
        {
            // Command = "s" (for Sensor)
            // Response = a number, or "error"

            return connection.ExecuteCommand("s");
        }

        /// <summary>
        /// Returns "ON" or "OFF" or "error" if the pin state could not be obtained. 
        /// </summary>
        /// <returns></returns>
        public string GetPinState()
        {
            // Command = "p" (for Pinstate)
            // Response = "On", "Off", or "error"
            return connection.ExecuteCommand("p");
        }

        /// <summary>
        /// Returns the Sensor Criteria number or "error". 
        /// </summary>
        /// <returns></returns>
        public string GetSensorCriteria()
        {
            // Command = "c"
            // Response = number
            return connection.ExecuteCommand("c");
        }

        /// <summary>
        /// Get the Criteria Mode. Returns "auto" if the Arduino automatically activates based on sensor criteria, "manual" if the user can activate using the app, or "error".
        /// </summary>
        /// <returns></returns>
        public string GetCriteriaMode()
        {
            // Command = "m" (for criteria Mode)
            // Response = "00a" (auto) or "00m" (manual) (00 because of dumb 4 char encoding stuff)

            string response = connection.ExecuteCommand("m");

            if (response == "00a") 
                return "auto";

            else if (response == "00m") 
                return "manual";

            else return response;
        }

        /// <summary>
        /// Toggles the pin state and returns the new state (ON or OFF), or "error" if the state could not be changed. 
        /// </summary>
        /// <returns></returns>
        public string TogglePinState()
        {

            // Command = "t" (for Toggle)
            // Response = "ON" or "OFF" (the new changed pin)

            string response = connection.ExecuteCommand("t");

            Status.PinState = response;
            return response;
        }

        /// <summary>
        /// Sets the criteria for when Arduino needs to turn pins ON when it reaches this value.
        /// Returns "error" if something went wrong.
        /// </summary>
        /// <param name="criteria">The sensor criteria that should be met on the Arduino.</param>
        /// <returns></returns>
        public string SetSensorCriteria(int criteria)
        {
            // int is used to make sure it's a valid number

            // Command = "s=X" where X is the number for the sensor criteria, like "s=100"
            // Response = "sOk" if the critera is set, or "error" if it could not be set

            string response = connection.ExecuteCommand("s=" + criteria);

            if (response == "sOk")
            {
                Status.SensorCriteria = Convert.ToString(criteria);
            }

            return response;
        }

        /// <summary>
        /// Set the Criteria Mode to "a" (auto) or "m" (manual). Returns the new mode, or "error" if the mode could not be changed. 
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when mode does not equal "a" or "m".</exception>
        /// <param name="mode">The new mode, must equal "a" or "m".</param>
        /// <returns></returns>
        public string SetCriteriaMode(string mode)
        {
            // Command = ma or mm (for Mode Automatic, or Mode Manual)
            // Response = "00m" or "00a" (the NEW criteria mode)

            if (mode != "a" && mode != "m")
            {
                throw new ArgumentException("Mode must specifically equal 'a' or 'm'.");
            }

            string response = connection.ExecuteCommand("m" + mode);

            if (response == "00a" || response == "00m")
            {
                Status.CriteriaMode = response == "00a" ? "auto" : "manual";
            }

            return response;
        }

    }
}
