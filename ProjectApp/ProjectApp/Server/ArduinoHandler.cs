using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProjectApp.Server
{
    class ArduinoHandler
    {
        /// <summary>ArduinoStatus object that can be used as a DataBinding object for the user interface to automatically update and display data.</summary>
        private Connection connection;

        
        /// <summary>Returns an ArduinoHandler object. Invoke StartConnection() to start a connection with the server.</summary>
        public ArduinoHandler()
        {
            connection = new Connection();
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
            if (connection.IsConnected())
                return false;

            // Try to start connection
            try
            {
                // Start connection
                connection.StartConnection(ipAddress, port);

                // Initialize values
                RefreshStatus();
            }

            // Something went wrong (like invalid IP or port)
            catch
            {
                return false;
            }

            return connection.IsConnected();
        }

        /// <summary>
        /// Refreshes the status (connected / not connected, and the sensor value).
        /// </summary>
        public void RefreshStatus()
        {
            // Not used right now    
        }

        /// <summary>
        /// Returns whether or not there is a connection with the Arduino going on. 
        /// </summary>
        public bool IsConnected()
        {
            return connection.IsConnected();
        }

        /// <summary>
        /// Call the CheckForDoorBell function in the connector
        /// </summary>
        /// <returns> wheter or not the bell was pressed</returns>
        public bool CheckForDoorBell()
        {
            connection.CheckForDoorBell(out bool success);
            return success;
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
         * Example:
         * GetSensorValue()     - "s" - Value
         * 
         */


        // Example:

        /// <summary>
        /// Returns the value from the sensor, or "error" if it could not be obtained.
        /// </summary>
        /// <returns></returns>
        public string GetSensorValue()
        {
            // Command = "s" (for Sensor)
            // Response = a number, or "error"

            // Returns sensor value, or "error" if it could not be retrieved
            return connection.ExecuteCommand("s");
        }


    }
}
