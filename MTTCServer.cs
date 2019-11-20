using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BqlMQtt
{
    public delegate void onMessageDelegate(Socket sock, string message);
    public class MTTCServer
    {

        private int port;
        TcpListener listener;
        public onMessageDelegate onMessage;
        public MTTCServer(int port)
        {
            this.port = port;
        }

        public void start()
        {
            Console.WriteLine("MTTCServer Started at port "+port);
            IPAddress address = IPAddress.Parse("0.0.0.0");
            listener = new TcpListener(address, port);
            listener.Start();
            Task.Run(new Action(startAsyncFunction));
        }

       private void startAsyncFunction()
        {
            while (true)
            {
                loop();
            }
        }
        private void loop()
        {
            Socket socket = listener.AcceptSocket();
            var mttc = new MTTCSocket(socket);
            mttc.onMessage = onMessage;
        }


    }
}
