using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Net.Sockets;

namespace smanager_server
{

    class socket_server_smanager
    {
        public static string data = null;
        string serverip = "";
        string serverport = "";

        public socket_server_smanager (string serverip, string serverport)
        {
            this.serverip = serverip;
            this.serverport = serverport;
        }



        public void startServer()
        {
            // Data buffer for incoming data.  
            byte[] bytes = new Byte[1024];

            // Establish the local endpoint for the socket.  
            // Dns.GetHostName returns the name of the   
            // host running the application.  
            IPAddress ipAddress = System.Net.IPAddress.Parse(serverip);
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 5842);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and   
            // listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                // Start listening for connections.  
                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");

                    Socket handler = listener.Accept();

                    server_smanager clientThread = new server_smanager(handler);
                    Thread t = new Thread(new ThreadStart(clientThread.doTask));
                    t.Start();

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();
        }
    }
}
