using System;
using System.Threading;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace smanager_server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public bool trydb()
        {
            string connStr = string.Format("server=localhost;user=root;password=;database=smanager");
            MySqlConnection Conn = new MySqlConnection(connStr);
            Conn.Open();
            if (Conn.State == ConnectionState.Open)
            {
                return true;
            }
            else
            {
                return false;
            }

            Conn.Close();
        }

        public void getInterfaces()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            if (host != null)
            {
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                        comboip.Items.Add(ip.ToString());
                }
                comboip.Items.Add("127.0.0.1");
            }
            else
                comboip.Items.Add("127.0.0.1");
        }

        public void start()
        {
            socket_server_smanager listener = new socket_server_smanager(comboip.Text, "5842");
            Thread thread = new Thread(new ThreadStart(listener.startServer));
            thread.Start();
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            if (btn_start.Text == "Start")
            {
                if(comboip.Text != "")
                {
                    if(trydb())
                    {
                        /*Thread thread = new Thread(new ThreadStart(start));
                        thread.Start();*/
                        start();
                        lbl_statusserver.Text = "Connected and Online";
                        lbl_statusserver.ForeColor = Color.Green;
                        comboip.Enabled = false;
                    }
                    else
                    {
                        lbl_statusserver.Text = "DB Error";
                        lbl_statusserver.ForeColor = Color.Red;
                    }
                }
                else
                {
                    MessageBox.Show("Seleziona l'indirizzo ip");
                }
            } 
            else if (btn_start.Text == "Stop")
            {

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            getInterfaces();
            lbl_statusserver.Text = "No Connection";
            lbl_statusserver.ForeColor = System.Drawing.Color.OrangeRed;
        }
    }
}
