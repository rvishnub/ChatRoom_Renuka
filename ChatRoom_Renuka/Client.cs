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
    public class Client
    {
        public string userName;


        public Client client;
        public TcpClient tcpClient;
        public Server server;
        public NetworkStream stream;
        public Dictionary dict;
        public Messages message;


        public Client(NetworkStream stream, TcpClient tcpClient)
        {
            this.stream = stream;
            tcpClient = this.tcpClient;
        }

        public void WelcomeUser()
        {

            string responseStr = "You are connected to Renuka's Chat Room.  Please send your user name.";
            byte[] responseByt = Encoding.ASCII.GetBytes(responseStr+" has joined.");
            stream.Write(responseByt, 0, responseByt.Length);
            int numberOfBytesReceived = server.stream.Read(server.buffer, 0, 256);
            userName = System.Text.Encoding.ASCII.GetString(server.buffer, 0, numberOfBytesReceived);
            Console.WriteLine(userName);
        }



        public void ReceiveMessages()
        {
            int numberOfBytesReceived = this.stream.Read(server.buffer, 0, 256);
            string messageStr = System.Text.Encoding.ASCII.GetString(server.buffer, 0, numberOfBytesReceived);
            message.queue.Add(messageStr);

        }

}
