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
    public class Messages
    {

        public Dictionary dict;
        TcpClient tcpClient;
        Client client;
        Server server;

        public List<string> queue = new List<string>();

        public Messages()
        {

        }

        public void SendMessageFromQueue()
        {
            string messageToBeSent = queue[0];
            Broadcast(messageToBeSent);
            queue.Remove(queue[0]);

        }

        public void Broadcast(string messageStr)
        {
            IPEndPoint groupEP = new IPEndPoint((IPAddress.Parse("10.2.20.16")), 0);
            EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
            byte[] messageByt = Encoding.ASCII.GetBytes(messageStr);

            foreach (string name in dict.userKeys)
            {
                Client user = dict.GetActiveUser(name);

                IPEndPoint userEP = (IPEndPoint)user.tcpClient.Client.RemoteEndPoint;

                if (user.tcpClient.Connected == true)
                {
                    user.stream.Write(messageByt, 0, messageByt.Length);
                    Console.WriteLine("Sent: {0}", Encoding.ASCII.GetString(messageByt));
                }
                else
                {
                    Console.WriteLine("{0} is disconnected", client.userName);

                }
            }


            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
         

        }

    

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < dict.userKeys.Count; i++)
            {
                yield return dict.userKeys[i];
            }

        }

    }
}
