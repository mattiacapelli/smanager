using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace client_smanager
{
    public partial class Form1 : Form
    {
        string keypath = "./auth.key";
        string auth_key = "";
        string clientid = "";
        socket_smanager ssmanager = new socket_smanager();

        public Form1()
        {
            InitializeComponent();
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public bool StartConnection()
        {
            return false;
        }

        public void authenticate(/*string key*/) 
        {
            Thread authThread = new Thread(new ThreadStart(ssmanager.sendAuth(auth_key, clientid, GetLocalIPAddress()));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists(keypath))
            {
                //Change the btn_action Text and update the lbl_auth text and color
                btn_action.Text = "Authenticate";
                lbl_auth.Text = "Not Authenticated";
                lbl_auth.ForeColor = Color.Red;
                lbl_connection.Text = "Waiting for Authentication";
                lbl_connection.ForeColor = Color.OrangeRed;
            }
            else
            {
                btn_action.Text = "Start";
                lbl_auth.Text = "Authenticated";
                lbl_auth.ForeColor= Color.Green;
                lbl_connection.Text = "No connection";
                lbl_connection.ForeColor = Color.OrangeRed;
            }
        }

        private void btn_action_Click(object sender, EventArgs e)
        {
            switch (btn_action.Text)
            {
                case "Authenticate":

                    //Try to authenticate to the server
                    if (authenticate())
                    {
                        lbl_auth.Text = "Authenticated";
                        lbl_auth.ForeColor = Color.Green;
                        btn_action.Text = "Start";
                    }
                    else
                    {
                        lbl_auth.Text = "Wrong Key";
                        lbl_auth.ForeColor = Color.Red;
                        btn_action.Enabled = false;
                        Thread.Sleep(5000);
                        btn_action.Enabled = true;
                        lbl_auth.Text = "Not Authenticated";
                        lbl_auth.ForeColor = Color.Red;
                    }

                    break;

                case "Start":

                    if (StartConnection())
                    {
                        lbl_connection.Text = "Connection Established";
                        lbl_connection.ForeColor = Color.Green;
                    }
                    else
                    {
                        lbl_connection.Text = "Error during the connection";
                        lbl_connection.ForeColor = Color.Red;
                    }

                    break;

                case "Stop":

                    break;
            }
        }
    }
}
