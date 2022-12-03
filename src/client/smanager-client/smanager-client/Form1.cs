using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace smanager_client
{
    public partial class Form1 : Form
    {
        Thread logthread = new Thread(new ThreadStart(fnull));
        
        public static void fnull() { }
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

        public void start()
        {
            if (txt_authkey.Text != "" && txt_ipaddress.Text != "")
            {
                client_smanager ssmanager = new client_smanager(txt_ipaddress.Text, txt_authkey.Text);
                logthread = new Thread(new ThreadStart(ssmanager.sendPacket));
                logthread.Start();
            }
            else
            {
                MessageBox.Show("Completa tutti i Campi");
            }
        }
        
        private void btn_start_Click(object sender, EventArgs e)
        {

            start();
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            if (logthread.IsAlive)
            {
                logthread.Abort();
            }
            else
            {
                MessageBox.Show("Avvia una connessione prima di Arrestarla");
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            logthread.Abort();
            System.Windows.Forms.Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
        }
    }
}
