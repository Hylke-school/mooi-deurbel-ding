using ProjectApp.Views;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace ProjectApp.Server
{
    class Connection
    {

        private IPAddress ipAddressServer;
        private int port;
        private IPEndPoint remoteEP;

        private Socket socket;
        public string data = null;


        public static int counter = 0;

        /// <summary>
        /// Returns a Connection object. You still have to manually invoke StartConnection() to establish a connection. 
        /// </summary>
        public Connection()
        {
            socket = null;
        }

        /// <summary>
        /// Attempts to establish a connection with the Arduino server.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when: ipAddress is valid, port is invalid, or a connection could not be made.</exception>
        /// <param name="ipAddress">The IP address of the server.</param>
        /// <param name="port">The port of the server.</param>
        public void StartConnection(string ipAddress, string port)
        {
            if (socket != null)
            {
                return;
            }

            if (CheckValidIpAddress(ipAddress) == false)
            {
                throw new ArgumentException("Invalid IP Address");
            }

            if (CheckValidPort(port) == false)
            {
                throw new ArgumentException("Invalid Port");
            }

            ipAddressServer = IPAddress.Parse(ipAddress);
            this.port = Convert.ToInt32(port);
            remoteEP = new IPEndPoint(ipAddressServer, this.port);

            try
            {
                socket = new Socket(ipAddressServer.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(remoteEP);
                ExecuteCommand("c", false);
            }
            catch
            {
                throw new ArgumentException("Could not connect to server");
            }
        }

        /// <summary>
        /// Closes the connection with the Arduino server.
        /// </summary>
        public void CloseConnection()
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }

        /// <summary>
        /// Returns whether the socket is connected or not. 
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            if (socket == null)
                return false;

            else return socket.Connected;
        }

        /// <summary>
        /// Executes a command by sending a message to the server and returning the response from the server (with white spaces trimmed). Returns "error" if something went wrong. 
        /// </summary>
        /// <param name="command">The command to send to the server/</param>
        /// <returns></returns>
        public string ExecuteCommand(string command, bool receiveData = true)
        {

            // Assuming response is always 4 bytes
            byte[] buffer = new byte[4];
            int bytesReceived = 0;
            string result = "error";

            if (socket != null)
            {
                SendMessage(command);
                try
                {
                    if (receiveData)
                    {
                        bytesReceived = socket.Receive(buffer);

                        if (bytesReceived == 4)
                        {
                            // -1 to skip \n, Trim() to trim white spaces at the start since it's always 4 characters (though whitespaces later got removed so probably unnecessary now)
                            result = Encoding.ASCII.GetString(buffer, 0, bytesReceived - 1).Trim();
                        }
                    }
                    else
                    {
                        result = "Success";
                    }
                }
                catch
                {
                    result = "error";
                }
            }

            return result;
        }

        /// <summary>
        /// Listens if the arduino has send a message, if the message is BEL, tell the arduino that the message is received.
        /// </summary>
        /// <param name="success">bool, if the message was received true otherwise false</param>
        public bool CheckForDoorBell()
        {
            byte[] bytes = new byte[1024];

            if (socket != null)
            {
                socket.ReceiveTimeout = 1;

                try
                {
                    data = null;

                    int bytesRec = socket.Receive(bytes);

                    if (bytesRec > 0)
                    {
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);

                        if (data.IndexOf("BEL") > -1)
                        {
                            counter++;
                            ExecuteCommand("r", false);
                            return true;
                        }
                    }
                }
                catch 
                {
                    throw new ArgumentException("Could not check for the doorbell");
                }
            }

            return false;
        }

        // ===================================================
        // ================ PRIVATE FUNCTIONS ================
        // ===================================================

        /// <summary>
        /// Sends a message to the Arduino server, encoded in such a way that messages always end with ">".
        /// </summary>
        /// <param name="message">The message to send.</param>
        private void SendMessage(string message)
        {
            // Messages always end with ">"
            byte[] msgAsBytes = Encoding.ASCII.GetBytes(message + ">");
            socket.Send(msgAsBytes);
        }

        /// <summary>
        /// Returns whether an IP address is valid or not. Code from Sibbele Oosterhaven.
        /// </summary>
        /// <param name="ip">The IP address to check</param>
        /// <returns></returns>
        private bool CheckValidIpAddress(string ip)
        {
            if (ip == "")
                return false;

            //Check user input against regex
            Regex regex = new Regex("\\b((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\\.|$)){4}\\b");
            Match match = regex.Match(ip);
            return match.Success;
        }

        /// <summary>
        /// Returns whether a port is a valid port. Code from Sibbele Oosterhaven. 
        /// </summary>
        /// <param name="port">The port to check</param>
        /// <returns></returns>
        private bool CheckValidPort(string port)
        {
            //Check if a value is entered.
            if (port == "") 
                return false;

            Regex regex = new Regex("[0-9]+");
            Match match = regex.Match(port);

            if (match.Success)
            {
                int portAsInteger = Int32.Parse(port);

                //Check if port is in range.
                return ((portAsInteger >= 0) && (portAsInteger <= 65535));
            }
            else return false;
        }
    }
}
