using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            server.Start();



            try
            {

                //CREATE BUFFER
                chatroom.buffer = new Byte[100];
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

                    //WELCOME
                    int numberOfBytesReceived = stream.Read(chatroom.buffer, 0, 100);
                    messageStr = System.Text.Encoding.ASCII.GetString(chatroom.buffer, 0, numberOfBytesReceived);
                    Console.WriteLine("A message has been received:");
                    Console.WriteLine(messageStr);

                    string responseStr = "You are connected to Renuka's Chat Room.";
                    byte[] responseByt = chatroom.ToBytes(responseStr);
                    stream.Write(responseByt, 0, responseByt.Length);

                    //chatroom.SetUserName();

                    responseStr = "What is your user name?";
                    Console.WriteLine(responseStr);
                    responseByt = chatroom.ToBytes(responseStr);
                    stream.Write(responseByt, 0, responseByt.Length);

                    numberOfBytesReceived = stream.Read(chatroom.buffer, 0, 100);
                    string userName = System.Text.Encoding.ASCII.GetString(chatroom.buffer, 0, numberOfBytesReceived);
                    Console.WriteLine(userName);

                    //chatroom.SetUserIPAddress(chatroom.userName);

                    //ADD TO DICTIONARY
                    dict.dictForUsers.Add(userName, client);

                    //DO CHAT
                    //Thread chatThread = new Thread(chatroom.DoChat);
                    //chatThread.Start();

                    numberOfBytesReceived = 1;
                    while ((numberOfBytesReceived) != 0)
                    {
                        //PARSE TO STRING
                        numberOfBytesReceived = stream.Read(chatroom.buffer, 0, 100);
                        messageStr = System.Text.Encoding.ASCII.GetString(chatroom.buffer, 0, numberOfBytesReceived);
                        Console.WriteLine("A message has been received:");
                        Console.WriteLine(messageStr);

                        chatroom.Broadcast(messageStr);
                        Console.WriteLine("The message has been broadcast to all users.");
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

