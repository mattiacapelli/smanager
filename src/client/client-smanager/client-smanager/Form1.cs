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

        public bool authenticate(string key) 
        {
            
            return true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists(keypath))
            {
                //Change the btn_action Text and update the lbl_auth text and color
                btn_action.Text = "Authenticate";
                lbl_auth.Text = "Not Authenticated";
                lbl_auth.ForeColor = Color.Red;
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

                    break;

                case "Stop":

                    break;
            }
        }
    }
}
