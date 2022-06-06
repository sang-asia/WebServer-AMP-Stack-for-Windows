using System;
using System.Net;
using System.Net.Sockets;

namespace WebServer.Libraries
{
    static class Network
    {
        /// <summary>
        /// Check a port available
        /// </summary>
        public static bool PortAvailable(int port)
        {
            IPAddress ipAddress = Dns.GetHostEntry("localhost").AddressList[0];
            TcpListener tcpListener;

            try
            {
                tcpListener = new TcpListener(ipAddress, port);
                tcpListener.Start();
                tcpListener.Stop();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
