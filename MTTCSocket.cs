using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BqlMQtt
{
   
    public class MTTCSocket
    {
        static ASCIIEncoding encoding = new ASCIIEncoding();
        Socket socket;
        public onMessageDelegate onMessage;
        public string cacheStr = "";
        public MTTCSocket(Socket sock)
        {
            this.socket = sock;
            IPEndPoint remoteIpEndPoint = sock.RemoteEndPoint as IPEndPoint;
            Console.WriteLine("Client["+ remoteIpEndPoint.Address+"] Connected to MTTCServer");
            Task.Run(new Action(asyncFunction));
        }

        private void asyncFunction()
        {
            while (true)
            {
                loop();
            }
        }
        private void loop()
        {
            if (this.socket.Available < 4)
                return;
            byte[] data = new byte[1024];
            int len = socket.Receive(data);
            string str = encoding.GetString(data,0, len);
            if (str.EndsWith("\r\n"))
            {
                onMessage.Invoke(socket, cacheStr + str);
                cacheStr = "";
            }
            else
            {
                cacheStr += str;
            }
                
        }


    }
}
