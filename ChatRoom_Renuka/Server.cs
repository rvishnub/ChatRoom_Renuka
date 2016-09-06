using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections;


namespace ChatRoom_Renuka
{
    public class Server
    {
        public IPAddress ipAddress;
        public int port;


        public TcpListener listener;
        public NetworkStream stream;
        public TcpClient tcpClient;
        public Dictionary dict;
        public Messages message;
        public Client client;
        public byte[] buffer;


        public Server()
        {

        }

        public void Run()
        {
            Console.WriteLine("Welcome to Renuka's Chat Room.");
            dict = new Dictionary();
            listener = new TcpListener(IPAddress.Parse("192.168.0.19"), 9218);
            Thread listenerStartThread = new Thread(listener.Start);
            listenerStartThread.Start();
            Console.WriteLine("Waiting for a connection. . . .");

            int counter = 0;
            while (true)
            {
                counter += 1;
                tcpClient = listener.AcceptTcpClient();
                stream = tcpClient.GetStream();
                client = new Client(stream, tcpClient);
                client.WelcomeUser();
                dict.dictForUsers.Add(client.userName, client);
                buffer = new Byte[256];


                message = new Messages();
                int numberOfBytesReceived = 0;
                while ((numberOfBytesReceived) != 0)
                {
                    client.ReceiveMessages();
                    message.SendMessageFromQueue();

                }

            }
        }


    }
}

            
