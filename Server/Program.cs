using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    public class ServerObject
    {
        static TcpListener tcpListener;
        List<ClientObject> clients = new List<ClientObject>();

        protected internal void AddConnection(ClientObject client)
        {
            clients.Add(client);
        }
        protected internal void RemoveConnection(string id)
        {
            ClientObject client = clients.FirstOrDefault(x => x.Id == id);
            if (client != null)
                clients.Remove(client);
        }

        protected internal void Listen()
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, 8888);
                tcpListener.Start();

                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Server start!");
                Console.BackgroundColor = ConsoleColor.Black;

                while (true)
                {
                    TcpClient tmp = tcpListener.AcceptTcpClient();

                    ClientObject clientObject = new ClientObject(tmp, this);
                    Thread clientThread = new Thread(new ThreadStart(clientObject.Process));
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Disconnect();
            }
        }
        protected internal void BroadcastMsg(string msg, string id)
        {
            byte[] data = Encoding.Unicode.GetBytes(msg);
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].Id != id)
                    clients[i].Stream.Write(data, 0, data.Length);
            }
        }
        protected internal void PrivatMsg(string msg, string id)
        {
            byte[] data = Encoding.Unicode.GetBytes(msg);
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].Id == id)
                    clients[i].Stream.Write(data, 0, data.Length);
            }
        }
        protected internal void Disconnect()
        {
            tcpListener.Stop();
            for (int i = 0; i < clients.Count; i++)
            {
                clients[i].Close();
            }
            Environment.Exit(0);
        }
    }
    public class ClientObject
    {
        public string Id { get; set; }
        public NetworkStream Stream { get; set; }
        string Name;
        TcpClient client;
        private ServerObject server;

        public ClientObject(TcpClient client, ServerObject serverObject)
        {
            this.client = client;
            this.server = serverObject;
            this.Id = Guid.NewGuid().ToString();
            server.AddConnection(this);
        }

        internal void Process()
        {
            try
            {
                Stream = client.GetStream();
                string msg = GetMsg();

                while (true)
                {
                    msg = GetMsg();
                    try
                    {
                        using (SqlConnection conn = new SqlConnection("Data Source=10.0.1.200;Initial Catalog=Chervonookiy_db;User Id=student;Integrated Security=False;"))
                        {
                            conn.Open();
                            using (SqlCommand command = new SqlCommand("SELECT Hour, Minute WHERE Ip=" + msg + " FROM TableSleepCalc;", conn))
                            {
                                SqlDataReader reader = command.ExecuteReader();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    Console.Write(reader.GetName(i).ToString() + " | ");
                                }
                                while (reader.Read())
                                {
                                    Console.WriteLine();
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        Console.Write(reader.GetValue(i).ToString() + " | ");

                                    }
                                }

                            }
                        }
                        msg = GetMsg();
                        msg = "Ллжитесь спать в:" + (int.Parse(msg)-9);
                        Console.WriteLine("get user data: " + msg);
                        server.PrivatMsg(msg, this.Id);
                    }
                    catch (Exception ex)
                    {
                        msg = Name + ": Exeption";
                        Console.WriteLine(msg);
                        server.PrivatMsg(msg, this.Id);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                server.RemoveConnection(this.Id);
                Close();
            }
        }

        private string GetMsg()
        {
            byte[] data = new byte[64];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;

            do
            {
                bytes = Stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            } while (Stream.DataAvailable);

            return builder.ToString();
        }

        internal void Close()
        {
            if (Stream != null)
                Stream.Close();
            if (client != null)
                client.Close();
        }
    }
    class Program
    {
        static ServerObject server;
        static Thread listenThread;
        static void Main(string[] args)
        {
            try
            {
                server = new ServerObject();
                listenThread = new Thread(new ThreadStart(server.Listen));
                listenThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                server.Disconnect();
            }
        }
    }
}