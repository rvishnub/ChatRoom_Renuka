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
    public class Dictionary : IEnumerable
    {

        public Dictionary<string, Client> dictForUsers = new Dictionary<string, Client>();
        public Dictionary<string, Client> dictForServer = new Dictionary<string, Client>();
        public List<string> userKeys;
        public List<Client> userValues;

        Server server;
        TcpClient tcpClient;
        Client client;


        public Dictionary(Server server)
        {
            this.server = server;
        }
        public void AddNewUser(string name, Client client)
        {
            dictForUsers.Add(name, client);
            Console.WriteLine("{0} has been added to the active user list.", name);
        }

        public void AddClient(string name, Client client)
        {
            dictForServer.Add(name, client);
            Console.WriteLine("{0} has been added to the client database.", name);

        }

        public Client GetActiveUser(string name)
        {
            while (true)
            {
                int nameIndex = userKeys.IndexOf(name);
                Client client = userValues[nameIndex];
                return client;
            }
            Console.WriteLine("That name has not been found.");
        }


        public void FindActiveUser(string name)
        {
            userKeys = new List<string>(dictForUsers.Keys);
            if (userKeys.Contains(name))
            {
                try
                {
                    Console.WriteLine("The IP address for {0} is {1}.", name, GetUserIPAddress(name).ToString());

                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine("That name has not been found.");
                }
            }

        }
        public void DeleteClient(string name)
        {
            userKeys = new List<string>(dictForUsers.Keys);
            if (userKeys.Contains(name))
            {
                try
                {
                    Console.WriteLine("The user {0} will be removed.", name);
                    dictForServer.Remove(name);
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine("That name has not been found.");
                }
            }
        }

        public void SetUserIPEndPoint(Client client)
        {
            IPEndPoint userIPEndPoint = (IPEndPoint)client.tcpClient.Client.RemoteEndPoint;
        }

        public string GetUserIPAddressString(Client client)
        {
            IPAddress userIPAddress = ((IPEndPoint)client.tcpClient.Client.RemoteEndPoint).Address;
            byte[] userIPAddressByt = userIPAddress.GetAddressBytes();
            string userIPAddressStr = Encoding.ASCII.GetString(userIPAddressByt);
            return userIPAddressStr;
        }



        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < userKeys.Count; i++)
            {
                yield return userKeys[i];
            }

        }
    }
}
    



