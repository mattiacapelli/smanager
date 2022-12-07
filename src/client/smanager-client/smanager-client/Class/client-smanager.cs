using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using LibreHardwareMonitor.Hardware;

namespace smanager_client
{
    class client_smanager
    {
        string server_ip = "";
        string client_ip = "";
        string auth_key = "";
        string packet_type = "auth";
        int server_port = 5842;
        public client_smanager(string server_ip, string client_ip, string auth_key)
        {
            this.client_ip = client_ip;
            this.server_ip = server_ip;
            this.auth_key = auth_key;
        }

        public bool imlogged(string msg)
        {
            Dictionary<string, string> dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(msg);
            foreach(KeyValuePair<string, string> entry in dict)
            {
                switch (entry.Key)
                {
                    case "status":
                        if(entry.Value == "logged")
                        {
                            return true;
                        } 
                        else if (entry.Value == "error")
                        {
                            return false;
                        }
                        break;
                }
            }
            return false;
        }



        public void sendPacket()
        {

            log_smanager logger = new log_smanager(auth_key,client_ip);
            while (true)
            {
                byte[] bytes = new byte[2048];
                // Connect to a remote device.  
                try
                {
                    // Establish the remote endpoint for the socket.  
                    // This example uses port 11000 on the local computer.  
                    //IPAddress ipAddress = System.Net.IPAddress.Parse("192.168.197.1");
                    IPAddress ipAddress = System.Net.IPAddress.Parse(server_ip);
                    IPEndPoint remoteEP = new IPEndPoint(ipAddress, server_port);

                    // Create a TCP/IP  socket.  
                    Socket sender = new Socket(ipAddress.AddressFamily,
                        SocketType.Stream, ProtocolType.Tcp);

                    // Connect the socket to the remote endpoint. Catch any errors.  
                    try
                    {
                        sender.Connect(remoteEP);

                        Console.WriteLine("Socket connected to {0}",
                            sender.RemoteEndPoint.ToString());

                        // Encode the data string into a byte array.  
                        //byte[] msg = Encoding.ASCII.GetBytes(BuildPacketDataJson() );
                        string packet = "";
                        byte[] msg = Encoding.ASCII.GetBytes("Error" + "<EOF>");

                        int bytesSent = 0;
                        int bytesRec = 0;
                        string msg_received = "";

                        switch (packet_type)
                        {
                            case "auth":
                                packet = "{\"type\": \"" + "auth" + "\",\"authkey\": \"" + this.auth_key + "\",\"ip_address\": \"" + client_ip + "\"}";
                                Console.WriteLine(packet);
                                msg = Encoding.ASCII.GetBytes(packet + "<EOF>");

                                // Send the data through the socket.  
                                bytesSent = sender.Send(msg);

                                // Receive the response from the remote device.  
                                bytesRec = sender.Receive(bytes);
                                msg_received = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                                if (imlogged(msg_received))
                                {
                                    Console.WriteLine("Logged");
                                    this.packet_type = "data";
                                }
                                else
                                {
                                    Console.WriteLine("Error");
                                }
                                
                                Console.WriteLine(msg_received);

                                break;

                            case "data":
                                packet = logger.getHardData();
                                msg = Encoding.ASCII.GetBytes(packet + "<EOF>");

                                // Send the data through the socket.  
                                bytesSent = sender.Send(msg);

                                // Receive the response from the remote device.  
                                bytesRec = sender.Receive(bytes);
                                msg_received = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                                Console.WriteLine(msg_received);

                                break;

                        }

                        // Release the socket.  
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

                Thread.Sleep(1000);
            }
        }
        
    }
}
