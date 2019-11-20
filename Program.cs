using MQTTnet;
using MQTTnet.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BqlMQtt
{
    class Program
    {
        static void MessageInterceptor(MqttApplicationMessageInterceptorContext context)
        {
            Console.WriteLine("Client [" +context.ClientId+ "]Message to topic: "+context.ApplicationMessage.Topic);

        }
        static void onMessage(Socket  socket,string message)
        {
            Console.WriteLine("rev messabe");
            Console.WriteLine(message);
        }

        static void Main(string[] args)
        {
            var optionsBuilder = new MqttServerOptionsBuilder()
            .WithConnectionBacklog(100)
            .WithApplicationMessageInterceptor(MessageInterceptor)
            .WithDefaultEndpointPort(1884);

            var mqttServer = new MqttFactory().CreateMqttServer();
              mqttServer.StartAsync(optionsBuilder.Build());
            Console.WriteLine("\n\n\nMQTTSERVER - KMAVN\n\tGiay Nhap\n\n");
            Console.WriteLine("MQTTServer started at port 1884");
            var mttcServer = new MTTCServer(3030);
            mttcServer.start();
            mttcServer.onMessage += onMessage;


            while (true)
            {
                Thread.Sleep(3000);
            }
        }
 
    }
}
