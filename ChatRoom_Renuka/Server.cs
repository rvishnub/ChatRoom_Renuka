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
    public class Server
    {
        public byte[] responseByt;//this is what the server sends
        public string responseStr;
        public byte messageByt;//these are client statements
        public byte messageStr;
        public string userName;
        public IPAddress userIPAddress;


        public Server chatroom;
        public TcpListener server;
        public NetworkStream stream;
        public TcpClient client;
        public Dictionary dict;
        public byte[] buffer;

        public string GetUserName()
        {
            SendUserNameRequest();
            GetUserNameMessage();
            SetUserName();
            return userName;
        
        }

        public void SendUserNameRequest()
        {

            string responseStr = "What is your user name?";
            Console.WriteLine("What is your user name?");
            byte[] responseByt = Encoding.ASCII.GetBytes(responseStr);
            stream.Write(responseByt, 0, responseByt.Length);
        }

        public string GetUserNameMessage()
        { 
            int bytesReceived = stream.Read(chatroom.buffer, 0, chatroom.buffer.Length);
            int numberBytesInClientResponse = stream.Read(responseByt, 0, responseByt.Length);
            string clientResponseStr = Encoding.ASCII.GetString(responseByt);
            Console.WriteLine(clientResponseStr);
            return clientResponseStr;
        }

        public void SetUserName()
        {
            userName = GetUserNameMessage();
        }
        
        public IPAddress GetUserIPAddress(string nameUser)
        {
            IPAddress userIPAddress = ((IPEndPoint)client.Client.RemoteEndPoint).Address;
            return userIPAddress;

        }

        public void SetUserIPAddress(string nameUser)
        {
            userIPAddress = GetUserIPAddress(nameUser);
        }

        public byte[] ToBytes(string responseStr)
        {
            byte[] responseByt = Encoding.ASCII.GetBytes(responseStr);
            return responseByt;
        }

        public string ToString(byte[] messageByt)
        {
            string messageStr = System.Text.Encoding.ASCII.GetString(messageByt, 0, messageByt.Length);
            return messageStr;
        }
        public void Broadcast(byte[] messageByt)
        {
            IPEndPoint groupEP = new IPEndPoint((IPAddress.Parse("10.2.20.23")), 0);
            EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);

            try
            {
                foreach (string name in dict.userKeys)
                {
                    TcpClient user = dict.GetActiveUser(name);
                    IPEndPoint userEP = new IPEndPoint(userIPAddress, 0);

                    if (user.Connected == true)
                    {
                        stream.Write(messageByt, 0, messageByt.Length);
                        Console.WriteLine("Sent: {0}", ToString(messageByt));
                    }
                    else
                    {
                        Console.WriteLine("{0} is disconnected", user);

                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


    }
}

