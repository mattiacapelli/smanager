using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

using System.Threading.Tasks;

namespace client_smanager
{
    internal class socket_smanager
    {
        string serverip = "";
        int serverport = 5842;
        string auth_key = "";
        string clientid = "";

        public void sendAuth(string auth_key, string clientid, string serverip)
        {
            string packet = "{\"type\": \"" + "auth" + "\",\"authkey\": \"" + auth_key + "\",\"ip_address\": \"" + clientid + "\"}";

            byte[] bytes = new byte[1024];
            int count = 0;



            //Try the connection to authenticate the client
            try
            {
                string data = "";
                this.serverip = serverip;
                this.auth_key = auth_key;
                this.clientid = clientid;

                IPAddress ipAddress = System.Net.IPAddress.Parse(this.serverip);
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, serverport);

                Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    sender.Connect(remoteEP);

                    Console.WriteLine("Socket connected to {0}", sender.RemoteEndPoint.ToString());
                    
                    byte[] msg = Encoding.ASCII.GetBytes(packet);
                    
                    int bytesSent = sender.Send(msg);

                    int bytesReceived = sender.Receive(bytes);
                    Console.WriteLine("Received text = {0}", Encoding.ASCII.GetString(bytes, 0, bytesReceived));

                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            } 
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return true;
        }
    }
}
