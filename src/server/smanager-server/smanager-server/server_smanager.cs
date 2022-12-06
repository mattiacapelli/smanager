using System;
using Newtonsoft.Json;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Net;
using System.Net.Sockets;
using System.Data;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Threading;


namespace smanager_server
{
    class server_smanager
    {
        Socket clientSocket;
        byte[] bytes = new Byte[1024];
        string data = "";

        public server_smanager(Socket Handler)
        {
            this.clientSocket = Handler;
        }

        public void doTask()
        {
            while (data != "quit$")
            {
                data = null;

                // An incoming connection needs to be processed.  
                while (true)
                {
                    int bytesRec = clientSocket.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }
                }

                // Show the data on the conscole.  
                Console.WriteLine("Text received : {0}", data);

                // Echo the data back to the client.  
                byte[] msg = Encoding.ASCII.GetBytes("This packet can't be processed");
                data = data.Remove(data.Length - 5);

                msg = Encoding.ASCII.GetBytes(data_void(data));


                clientSocket.Send(msg);

            }
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();

        }

        public string data_void(string data)
        {
            string type = "";
            string msg = "";
            Dictionary<string, string> dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);

            foreach (KeyValuePair<string, string> entry in dict)
            {
                switch (entry.Key)
                {
                    case "type":
                        type = entry.Value;
                        switch (type)
                        {
                            case "data":
                                msg = datalog(dict);
                                break;

                            case "auth":
                                msg = authenticate(dict);
                                break;

                            default:
                                msg = "This packet can't be processed";
                                break;
                        }

                        break;
                }
            }
            return msg;
        }

        public string authenticate(Dictionary<string, string> data)
        {
            string type = "";
            string authkey = "";
            string ip_address = "";
            foreach (KeyValuePair<string, string> entry in data)
            {
                switch (entry.Key)
                {
                    case "type":
                        type = entry.Value.ToString();
                        break;

                    case "authkey":
                        authkey = entry.Value.ToString();
                        break;

                    case "ip_address":
                        ip_address = entry.Value.ToString();
                        break;

                    default:
                        break;
                }
            }

            return tryauth(type, authkey, ip_address);

        }

        public string tryauth(string type, string authkey, string ip_address)
        {
            string connStr = string.Format("server=localhost;user=root;password=;database=smanager");
            MySqlConnection Conn = new MySqlConnection(connStr);
            Conn.Open();
            if (Conn.State == ConnectionState.Open)
            {
                return tablesForLogin(Conn, type, authkey, ip_address);
            }
            else
            {
                return "Error during connection!";
            }

            Conn.Close();
        }
        public string tablesForLogin(MySqlConnection connection, string type, string authkey, string ip_address)
        {
            List<string> names = new List<string>();
            MySqlCommand cmdName = new MySqlCommand("select * from servers where authkey = '" + authkey + "' AND ip_address= '" + ip_address + "'", connection);
            MySqlDataReader reader = cmdName.ExecuteReader();
            if (reader.Read())
            {
                return "{\"type\": \"" + "auth" + "\",\"status\": \"" + "logged" + "\"}";
            }
            else
            {
                return "{\"type\": \"" + "auth" + "\",\"status\": \"" + "error" + "\"}";
            }
        }

        public string datalog(Dictionary<string, string> dict)
        {
            string type = "";
            string authkey = "";
            string datetime = "";
            string cpu = "";
            string ram_used = "";
            string ram_free = "";
            string ram_total = "";
            string vram_total = "";
            string vram_used = "";
            string vram_free = "";

            string ip_address = "";

            foreach (KeyValuePair<string, string> kvp in dict)
            {
                switch (kvp.Key)
                {
                    case "type":
                        type = kvp.Value;
                        break;

                    case "authkey":
                        authkey = kvp.Value;
                        break;

                    case "datetime":
                        datetime = kvp.Value;
                        break;

                    case "cpu":
                        cpu = kvp.Value;
                        break;

                    case "ram_used":
                        ram_used = kvp.Value;
                        break;

                    case "ram_free":
                        ram_free = kvp.Value;
                        break;

                    case "ram_total":
                        ram_total = kvp.Value;
                        break;

                    case "vram_total":
                        vram_total = kvp.Value;
                        break;

                    case "vram_used":
                        vram_used = kvp.Value;
                        break;

                    case "vram_free":
                        vram_free = kvp.Value;
                        break;

                    default:
                        return null;
                        break;
                }
            }
            return trylog(type, authkey, datetime, cpu, ram_used, ram_free, ram_total, vram_total, vram_used, vram_free);
        }

        public string trylog(string type, string authkey, string datetime, string cpu, string ram_used, string ram_free, string ram_total, string vram_total, string vram_used, string vram_free)
        {
            string connStr = string.Format("server=localhost;user=root;password=;database=smanager");
            using (MySqlConnection Conn = new MySqlConnection(connStr))
            {
                Conn.Open();
                if (Conn.State == ConnectionState.Open)
                {
                    return tablesForLog(Conn, type, authkey, datetime, cpu, ram_used, ram_free, ram_total, vram_total, vram_used, vram_free);
                }
                else
                {
                    return "Error during connection!";
                }

                Conn.Close();
            }
        }

        public string tablesForLog(MySqlConnection connection, string type, string authkey, string datetime, string cpu, string ram_used, string ram_free, string ram_total, string vram_total, string vram_used, string vram_free)
        {

            //This is my connection string i have assigned the database file address path
            string MyConnection2 = string.Format("server=localhost;user=root;password=;database=smanager");
            //This is my insert query in which i am taking input from the user through windows forms
            string Query = "insert into logs(authkey, datetime, cpu, ram_used, ram_free, ram_total, vram_total, vram_used, vram_free) values('" + authkey + "','" + datetime + "','" + cpu + "','" + ram_used + "','" + ram_free + "','" + ram_total + "','" + vram_total + "','" + vram_used + "','" + vram_free + "');";
            //This is  MySqlConnection here i have created the object and pass my connection string.
            MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
            //This is command class which will handle the query and connection object.
            MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
            MySqlDataReader MyReader2;
            MyConn2.Open();
            MyReader2 = MyCommand2.ExecuteReader();     // Here our query will be executed and data saved into the database.
            while (MyReader2.Read())
            {
            }
            MyConn2.Close();
            return "ciao";

        }
    }
}
