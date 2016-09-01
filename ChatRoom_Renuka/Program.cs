using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections;

namespace ChatRoom_Renuka
{
    class Program
    {
        static void Main(string[] args)
        {
            Server chatroom = new Server();
            IPAddress ipAddress = IPAddress.Parse("10.2.20.21");
            int port = 9218;
            TcpListener server = new TcpListener(ipAddress, port);


            try
            {
                server.Start();

                //CREATE BUFFER
                chatroom.buffer = new Byte[1024];
                string messageStr = "";
                Dictionary dict = new Dictionary(chatroom);

                //LISTEN
                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    //ACCEPT CLIENT
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected to a User!");


                    //string messageStr = null;  DO I NEED THIS?

                    //CREATE STREAM
                    NetworkStream stream = client.GetStream();

                    int numberOfBytesRead = 0;
                    //WELCOME

                    messageStr = chatroom.ToString(chatroom.buffer);
                    Console.WriteLine("Received: '{0}'", messageStr);

                    //// Process the data sent by the client.  DO I NEED THIS?
                    //messageStr = messageStr.ToUpper();

                    string responseStr = "You are connected to Renuka's Chat Room.";
                    byte[] responseByt = chatroom.ToBytes(responseStr);
                    //chatroom.SetUserName();

                    responseStr = "What is your user name?";
                    Console.WriteLine(responseStr);
                    responseByt = Encoding.ASCII.GetBytes(responseStr);
                    stream.Write(responseByt, 0, responseByt.Length);

                    int bytesReceived = stream.Read(chatroom.buffer, 0, chatroom.buffer.Length);
                    int numberBytesInClientResponse = stream.Read(responseByt, 0, bytesReceived);
                    string clientResponseStr = Encoding.ASCII.GetString(responseByt);
                    Console.WriteLine(clientResponseStr);


                    //chatroom.SetUserIPAddress(chatroom.userName);
                    IPAddress userIPAddress = ((IPEndPoint)client.Client.RemoteEndPoint).Address;
                    Console.WriteLine(userIPAddress);


                    //DO CHAT
                    while ((numberOfBytesRead = stream.Read(chatroom.buffer, 0, chatroom.buffer.Length)) != 0)
                    {
                        //PARSE TO STRING
                        byte[] messageByt = chatroom.buffer;
                        messageStr = chatroom.ToString(messageByt);
                        Console.WriteLine("Received: '{0}'", messageStr);

                        chatroom.Broadcast(messageByt);

                    }

                    //CLOSE THE CONNECTION
                    client.Close();
                }
            }

            finally
            {
                //SHUT DOWN THE SERVER
                server.Stop();
            }















        }
    }
}
