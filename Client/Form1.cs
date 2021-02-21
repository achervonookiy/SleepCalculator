using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        private const string host = "127.0.0.1";
        private const int port = 8888;
        static TcpClient client;
        static NetworkStream stream;

        public Form1()
        {
            InitializeComponent();
            comboBoxHourSleep.SelectedIndex = 0;
            comboBoxMinSleep.SelectedIndex = 0;

            //client = new TcpClient();
            //try
            //{
            //    client.Connect(host, port);
            //    stream = client.GetStream();
            //    Thread receiveThread = new Thread(new ThreadStart(ReceiveMsg));
            //    receiveThread.Start();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private static void Disconnect()
        {
            stream?.Close();
            client?.Close();
            Environment.Exit(0);
        }

        void ReceiveMsg()
        {
            while (true)
            {
                try
                {
                    byte[] data = new byte[64];
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    } while (stream.DataAvailable);

                    string str = builder.ToString();                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Подключение прервано!");
                    Disconnect();
                }
            }

        }

        private void buttonSend_Click(object sender, EventArgs e)
        {

            if(!string.IsNullOrEmpty(textBoxIp.Text) && !Regex.IsMatch(textBoxIp.Text, @"[^a-zA-z\d_]")) {
                using (SqlConnection conn = new SqlConnection("Data Source=10.0.1.200;Initial Catalog=Chervonookiy_db;User Id=student;Integrated Security=False;"))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("UPDATE TableSleepCalc SET [Hour]=" + comboBoxHourSleep.SelectedItem + ", [Minute]=" + comboBoxMinSleep.SelectedItem + " WHERE [UserName]=\'" + textBoxIp.Text + "\';", conn))
                    {
                        int count = command.ExecuteNonQuery();
                        if (count > 0)
                            MessageBox.Show("OK");
                        else
                            addTableElement();
                    }
                }
            }
            else
            {
                MessageBox.Show("Username must not be empty and contain spaces with characters");
            }
            //string msg = textBoxIp.Text;
            //byte[] data = Encoding.Unicode.GetBytes(msg);
            //stream.Write(data, 0, data.Length);
        }
        public void addTableElement()
        {
            using (SqlConnection conn = new SqlConnection("Data Source=10.0.1.200;Initial Catalog=Chervonookiy_db;User Id=student;Integrated Security=False;"))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("INSERT INTO TableSleepCalc([UserName], [Hour], [Minute]) VALUES('" + textBoxIp.Text + "', '" + comboBoxHourSleep.SelectedItem + "', '" + comboBoxMinSleep.SelectedItem + "');", conn))
                {
                    int count = command.ExecuteNonQuery();
                    if (count > 0)
                        MessageBox.Show("OK");
                    else
                        MessageBox.Show("не OK");
                }
            }
        }
    }
}
