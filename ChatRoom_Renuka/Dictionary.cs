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
    public class Dictionary: IEnumerable
    {

        public Dictionary<string, TcpClient> dictForUsers = new Dictionary<string, TcpClient>();
        public Dictionary<string, TcpClient> dictForServer = new Dictionary<string, TcpClient>();
        public List<string> userKeys;
        public List<TcpClient> userValues;

        Server chatroom;
        TcpClient client;


        public Dictionary(Server myServer)
        {
            myServer = this.chatroom;
        }
        public void AddNewUser(string name, TcpClient client)
        {
            dictForUsers.Add(name, client);
            Console.WriteLine("{0} has been added to the active user list.", name);
        }

        public void AddClient(string name, TcpClient client)
        {
            dictForServer.Add(name, client);
            Console.WriteLine("{0} has been added to the client database.", name);

        }

        public TcpClient GetActiveUser(string name)
        {
            int nameIndex = userKeys.IndexOf(name);
            TcpClient value = userValues[nameIndex];
            return value;
        }
            //else
            //{
            //    Console.WriteLine("That name has not been found.");
            //}
        //}
            
        public void FindActiveUser(string name)
        {
            userKeys = new List<string>(dictForUsers.Keys);
            if (userKeys.Contains(name))
            {
                try
                {
                    Console.WriteLine("The IP address for {0} is {1}.", name, chatroom.GetUserIPAddress(name).ToString());

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

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}

